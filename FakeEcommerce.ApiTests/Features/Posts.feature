Feature: Posts API

Scenario: Get list of posts
	When user sends request to get posts
	Then response status code should be 200
	And response should contain posts
	And each post should have valid data

Scenario: Get post by id
	When user sends request to get post with id 1
	Then response status code should be 200
	And response should contain post with id 1

Scenario: Get post with invalid id
	When user sends request to get post with id 999999
	Then response status code should be 404

Scenario: Create new post
	When user creates a new post
	Then response status code should be 201
	And response should contain created post