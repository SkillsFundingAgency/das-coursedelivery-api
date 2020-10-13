Feature: ProviderCourses
	As a CourseDelivery API consumer
    I want to retrieve all Courses for a specific Provider
    So that I can use them in my own application

Scenario: Get Provider Courses
	Given I have an http client
    When I GET the following url: /api/providers/10000528/courses
    Then an http status code of 200 is returned
