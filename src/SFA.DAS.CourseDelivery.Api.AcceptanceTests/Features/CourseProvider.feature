Feature: CourseProvider
	As a CourseDelivery API consumer
    I want to retrieve a single Provider for a specific Course
    So that I can use them in my own application

@mytag
Scenario: Get Course Provider
	Given I have an http client
    When I GET the following url: /api/providers/
    Then an http status code of 200 is returned
    And all levels are returned