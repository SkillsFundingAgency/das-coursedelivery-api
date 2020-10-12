Feature: CourseProviders
	As a CourseDelivery API consumer
    I want to retrieve Providers for a specific Course
    So that I can use them in my own application

Scenario: Get Course Providers
	Given I have an http client
    When I GET the following url: /api/100/providers
    Then an http status code of 200 is returned
    And all course providers are returned