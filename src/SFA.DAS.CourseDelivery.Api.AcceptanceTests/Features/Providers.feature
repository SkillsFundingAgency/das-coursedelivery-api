Feature: Providers
	As a CourseDelivery API consumer
    I want to retrieve all Providers
    So that I can use them in my own application

Scenario: Get list of Providers
	Given I have an http client
    When I GET the following url: /api/providers
    Then an http status code of 200 is returned
    And all registered providers are returned
