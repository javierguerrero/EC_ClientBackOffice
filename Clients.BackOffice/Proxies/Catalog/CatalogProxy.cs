﻿using Clients.BackOffice.Proxies.Catalog.Commands;
using Clients.BackOffice.Proxies.Catalog.DTOs;
using Clients.BackOffice.Proxies.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clients.BackOffice.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task<DataCollection<CourseOverviewDto>> GetCourseOverviewsAsync(int page, int take);
        Task<CourseDto> GetCourseAsync(int id);

        Task CreateCourseAsync(CourseCreateCommand command);
        Task UpdateCourseAsync(CourseUpdateCommand command);

        Task RemoveCourseAsync(int id);

        Task<DataCollection<LessonOverviewDto>> GetLessonOverviewsAsync(int page, int take);

        Task<LessonDto> GetLessonAsync(int id);

        Task CreateLessonAsync(LessonCreateCommand lesson);

        Task<List<CharacterDto>> GetCharactersAsync();

        Task RemoveLessonAsync(int id);
    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CatalogProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        #region Course

        public async Task<DataCollection<CourseOverviewDto>> GetCourseOverviewsAsync(int page, int take)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}courses?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<CourseOverviewDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CourseDto> GetCourseAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}courses/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CourseDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateCourseAsync(CourseCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );
            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}courses", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateCourseAsync(CourseUpdateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );
            var request = await _httpClient.PatchAsync($"{_apiGatewayUrl}courses", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task RemoveCourseAsync(int id)
        {
            var request = await _httpClient.DeleteAsync($"{_apiGatewayUrl}courses/{id}");
            request.EnsureSuccessStatusCode();
        }

        #endregion Course

        #region Lesson

        public async Task<DataCollection<LessonOverviewDto>> GetLessonOverviewsAsync(int page, int take)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}lessons?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<LessonOverviewDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<LessonDto> GetLessonAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}lessons/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<LessonDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateLessonAsync(LessonCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}lessons", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task<List<CharacterDto>> GetCharactersAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}characters");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CharacterDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task RemoveLessonAsync(int id)
        {
            var request = await _httpClient.DeleteAsync($"{_apiGatewayUrl}lessons/{id}");
            request.EnsureSuccessStatusCode();
        }

        #endregion Lesson
    }
}