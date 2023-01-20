#pragma once
// R/WinRT (C++)
//
// Copyright (C) mntone.
// Licensed under the MIT License.

#ifndef IMPL
#define __SELF_IMPL
#define IMPL __impl
#endif

namespace IMPL {

#ifdef INIT_RESOURCE_FORMATTER_SIZE
constexpr size_t __nextPowerOf2(size_t val) {
	val = val - 1;
	while (val & val - 1) {
		val = val & val - 1;
	}
	return val << 1;
}
constexpr size_t kInitFormatterSize { __nextPowerOf2(INIT_RESOURCE_FORMATTER_SIZE) };
#else
constexpr size_t kInitFormatterSize { 63 };
#endif

struct Formatter final {
	_CONSTEXPR20 Formatter(): buffer_(kInitFormatterSize) {
		buffer_.push_back('\0');
	}

	template<class... Args>
	inline winrt::hstring Format(winrt::hstring const& format, Args... args) const {
		wchar_t const* fmtstr { format.c_str() };
		int const len { _scwprintf(fmtstr, args...) };
		WINRT_ASSERT(len != -1);
		buffer_.resize(len + 1);

		int const ret { swprintf_s(buffer_.data(), buffer_.size(), fmtstr, args...) };
		WINRT_ASSERT(ret != -1);
		return winrt::hstring(buffer_.data(), len);
	}

private:
	mutable ::std::vector<wchar_t> buffer_;
};

struct __ResourceManagerBase {
private:
	__ResourceManagerBase() = delete;

protected:
	inline __ResourceManagerBase(::std::wstring_view resourceName)
		: m_resourceManager()
		, m_resources(m_resourceManager.MainResourceMap().GetSubtree(resourceName)) {
	}

	inline ::winrt::hstring ValueCore(::winrt::param::hstring const& key) const {
		::winrt::hstring resourceValue { m_resources.GetValue(key).ValueAsString() };
		return resourceValue;
	}

	inline winrt::hstring ValueCore(::winrt::param::hstring const& key, ::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceContext const& context) const {
		::winrt::hstring resourceValue { m_resources.GetValue(key, context).ValueAsString() };
		return resourceValue;
	}

public:
	inline ::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceManager Native() const noexcept { return m_resourceManager; }

private:
	::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceManager const m_resourceManager;
	::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceMap const m_resources;
};

#ifdef USE_RESOURCE_CACHE
template<typename Resources>
struct __CacheableResourceManagerBase: public __ResourceManagerBase {
	inline __CacheableResourceManagerBase(::std::wstring_view resourceName)
		: __ResourceManagerBase(resourceName) {
		m_cache.reserve(static_cast<size_t>(Resources::__MaxCount));
	}

	template<Resources Key>
	inline winrt::hstring Value() const {
		return Value<Key>(nullptr);
	}

	template<Resources Key>
	inline winrt::hstring Value(::std::nullptr_t) const {
		auto itr { m_cache.find(Key) };
		if (itr == m_cache.cend()) {
			winrt::hstring resourceValue { ValueCore(__namespace_of<Resources>::res_of<Key>(nullptr)) };
			m_cache.emplace(Key, resourceValue);
			return resourceValue;
		}
		return itr->second;
	}

	template<Resources Key>
	inline winrt::hstring Value(::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceContext const& context) const {
		winrt::hstring resourceValue { ValueCore(__namespace_of<Resources>::res_of<Key>(nullptr), context) };
		return resourceValue;
	}

private:
	mutable ::std::unordered_map<Resources, ::winrt::hstring> m_cache;
};
template<typename Resources> using ResourceManagerBase = __CacheableResourceManagerBase<Resources>;
#else
template<typename Resources>
struct __PassthroughResourceManagerBase : public __ResourceManagerBase {
	inline __PassthroughResourceManagerBase(::std::wstring_view resourceName): __ResourceManagerBase(resourceName) { }

	template<Resources Key>
	inline winrt::hstring Value() const {
		return Value<Key>(nullptr);
	}

	template<Resources Key>
	inline winrt::hstring Value(::std::nullptr_t) const {
		winrt::hstring resourceValue { ValueCore(__namespace_of<Resources>::res_of<Key>(nullptr)) };
		return resourceValue;
	}

	template<Resources Key>
	inline winrt::hstring Value(::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceContext const& context) const {
		winrt::hstring resourceValue { ValueCore(__namespace_of<Resources>::res_of<Key>(nullptr), context) };
		return resourceValue;
	}
};
template<typename Resources> using ResourceManagerBase = __PassthroughResourceManagerBase<Resources>;
#endif

} // ^^^ namespace IMPL ^^^

template<typename Resources>
struct ResourceManager;

template<typename Resources>
struct ResourceContext {
	friend struct ResourceManager<Resources>;

private:
	ResourceContext() = delete;

	inline ResourceContext(ResourceManager<Resources> const& manager) noexcept
		: m_manager(manager)
		, m_resourceContext(manager.Native().CreateResourceContext()) {
	}

	inline ResourceContext(ResourceManager<Resources> const& manager, ::winrt::param::hstring const& language) noexcept
		: ResourceContext(manager) {
		m_resourceContext.QualifierValues().Insert(L"Language", language);
	}

public:
	inline void ChangeLanguage(::winrt::param::hstring const& language) {
		::winrt::Windows::Foundation::Collections::IMap<::winrt::hstring, ::winrt::hstring> qualifierValues { m_resourceContext.QualifierValues() };
		qualifierValues.Clear();
		qualifierValues.Insert(L"Language", language);
	}

	template<Resources Key>
	inline winrt::hstring Value() const {
		return m_manager.Value<Key>(m_resourceContext);
	}

	template<Resources Key, class... Args>
	::winrt::hstring Format(Args... args) const {
		::winrt::hstring resourceValue { m_manager.Value<Key>(m_resourceContext) };
		::winrt::hstring formattedValue { m_manager.Formatter().Format(resourceValue, args...) };
		return formattedValue;
	}

private:
	ResourceManager<Resources> const& m_manager;
	::winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceContext m_resourceContext;
};

template<typename Resources>
struct ResourceManager final: public IMPL::ResourceManagerBase<Resources> {
	inline ResourceManager()
		: IMPL::ResourceManagerBase<Resources>(IMPL::__namespace_of<Resources>::type_v) {
	}

	inline ResourceContext<Resources> Context() const noexcept {
		return ResourceContext<Resources>(*this);
	}

	inline ResourceContext<Resources> Context(::winrt::param::hstring const& language) const noexcept {
		return ResourceContext<Resources>(*this, language);
	}

	template<Resources Key, class... Args>
	inline ::winrt::hstring Format(Args... args) const {
		::winrt::hstring resourceValue { IMPL::ResourceManagerBase<Resources>::Value<Key>(nullptr) };
		::winrt::hstring formattedValue { m_formatter.Format(resourceValue, args...) };
		return formattedValue;
	}

public:
	IMPL::Formatter Formatter() const noexcept { return m_formatter; }

private:
	IMPL::Formatter m_formatter;
};

#ifdef __SELF_IMPL
#undef __SELF_IMPL
#undef IMPL
#endif
