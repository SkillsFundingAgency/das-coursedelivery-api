Feature: Shortlist
	As a CourseDelivery API consumer
    I want to retrieve all Shortlist items for a user
    So that I can use them in my own application

Scenario: Get list of Shortlist items
	Given I have an http client
    When I GET the following url: /api/shortlist/users/d6d467c4-28fb-4993-ac97-f1b1f865fd69
    Then an http status code of 200 is returned
    And all shortlist items for the user are returned