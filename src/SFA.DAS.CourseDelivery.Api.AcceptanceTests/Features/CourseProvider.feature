Feature: CourseProvider
	As a CourseDelivery API consumer
    I want to retrieve a single Provider for a specific Course
    So that I can use them in my own application

Scenario: Get Course Provider
	Given I have an http client
    When I GET the following url: /api/courses/10/providers/20002451
    Then an http status code of 200 is returned
    And specific course provider 20002451 is returned