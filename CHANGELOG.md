# Changelog
## 3.1.2
* Fix NullReferenceException with bvstate parameter

## 3.1.2
* Pass-through of User Agent to gather statistics.

## 3.1.1
* Upgrade Nvelocity version. This change requires DotNet Framework 3.5
* Include BVLog4Net.config in the release

## 3.1.0

* Added support for seller ratings.
* Fix - Debug logging has been turned off.

## 2.3.0.0

* Added support for bvstate parameter.
* Fixed 'sp_mt' metadata.

## 2.2.0.3

* Release notes and version update.

## 2.2.0.2

* Cleaned Spotlights unit tests.
* Fixed footer metadata.
* Disabled debug logging in BVLog4Net.config.

## 2.2.0.1

* Support for Spotlight content type has been added.
* QA support for internal purpose only.
* Fixed ct_st meta data tag in footer.
* Fixed master footer text.
* Unit tests are updated.

## 2.1.0.1

* BVParameters.pageNumber has been added. When used, this parameter will
override page number variables extracted from PageURI.
* BVClient.BOT_DETECTION has been removed. Bot detection functionality now only
applies to execution timeouts.
* BVClinet.EXECUTION_TIMEOUT_BOT has been added with default value of 2000ms,
which is the execution timeout intended for search bots. The minimum
configurable value for this timeout is 100ms.
* Default value for BVClient.EXECUTION_TIMEOUT is set to 500ms from its
original 3000ms. If this value is set to 0ms, no connection attempts will
occur.
* Default value for CONNECT_TIMEOUT and SOCKET_TIMEOUT is increased to 2000ms
(to match BVCLIENT.EXECUTION_TIMEOUT_BOT).
* Error handling for EXECUTION_TIMEOUT and EXECUTION_TIMEOUT_BOT
implementation.

## 2.1.0.0-beta-1

* Supports all version of .NET Framework from 2.0 and above.
* Property driven using default bvclient properties.
* Override bvclient properties option.
* Multiple configuration support to override bvclient properties.
* Parameters as object when accessing Bazaarvoice content API.* Simplified usage of Bazaarvoice content API.* Use Bazaarvoice contents API with default configuration or supply with user
configuration.* Include integration scripts.* Bazaarvoice support for Reviews, Question/Answers & Stories.* Content type support for category and products* StoryList and StoryGrid support for Stories.* User friendly error messages for most of the programmatic and known
scenarios.* Execution timeout support to stop execution, so when the execution of the
complete job is not finished in a given time, and cancel action and a display
a comment tag.* Crawler agent support for including multiple agents as normal text string so
user can set plain agent pattern in BVConfiguration. For multiple agent crawler
text, separate with `|` delimiter.* Connection timeout support for cloud SEO implementation.* Footer content support for displaying details of Bazaarvoice Configuration &
Parameters and for displaying debug information (with bvreveal=debug parameter
in the query string).* Validation support to validate BVConfiguration & BVParameters properties.* HTTP Proxy & SSL support for handling proxy servers among clients' firewalls.* Custom charset support to handle different client charsets. Custom charset
can be configured in BVConfiguration using BVClientConfig.CHARSET property.
