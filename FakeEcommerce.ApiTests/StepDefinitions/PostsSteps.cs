using FakeEcommerce.ApiTests.Models;
using FluentAssertions;
using System.Text.Json;
using FakeEcommerce.ApiTests.Clients;

namespace FakeEcommerce.ApiTests.StepDefinitions
{
    [Binding]
    public sealed class PostsApiStepDefinitions
    {
        private HttpResponseMessage _response;
        private HttpClient _client;
        private List<PostDto> _posts;
        private readonly PostsApiClient _postsApiClient = new();
                
        [When("user sends request to get posts")]
        public async Task WhenUserSendsRequestToGetPosts() //async? Task?
        {
            _response = await _postsApiClient.GetPosts();
        }

        [When("user sends request to get post with id (.*)")]
        public async Task WhenUserSendsRequestToGetPostWithID(int id)
        {            
            _response = await _postsApiClient.GetPostById(id);
        }
                
        [When("user creates a new post")]
        public async Task WhenUserCreatesANewPost()
        {
            var request = new CreatePostRequest
            {
                UserId = 1,
                Title = "Test title",
                Body = "Test body"
            };

            _response = await _postsApiClient.CreatePost(request);
        }

        [Then("response status code should be (.*)")]
        public void ThenResponseStatusCodeShouldBe(int statusCode)
        {
            ((int)_response.StatusCode).Should().Be(statusCode);
        }

        [Then("response should contain posts")]
        public async Task ThenResponseShouldContainPosts()
        {
            var content = await _response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            _posts = JsonSerializer.Deserialize<List<PostDto>>(content);

            _posts.Should().NotBeNull();
            _posts.Should().NotBeEmpty();
        }

        [Then("response should contain post with id (.*)")]
        public async Task ThenResponseShouldContainPostWithId(int id)
        {
            var content = await _response.Content.ReadAsStringAsync();

            content.Should().NotBeNullOrEmpty();

            var post = JsonSerializer.Deserialize<PostDto>(content);
            post.Should().NotBeNull();
            post.Id.Should().Be(id);
        }

        [Then("each post should have valid data")]
        public void ThenEachPostShouldHaveValidData()
        {
            _posts.Should().NotBeNull();

            foreach (var post in _posts)
            {
                post.Id.Should().BeGreaterThan(0);
                post.Title.Should().NotBeNullOrEmpty();
                post.Body.Should().NotBeNullOrEmpty();
            }
        }

        [Then("response should contain created post")]
        public async Task ThenResponseShouldContainCreatedPost()
        {
            var content = await _response.Content.ReadAsStringAsync();

            var post = JsonSerializer.Deserialize<PostDto>(content);

            post.Should().NotBeNull();
            post.Id.Should().BeGreaterThan(0);
            post.Title.Should().Be("Test title");
            post.Body.Should().Be("Test body");
        }
    }
}
