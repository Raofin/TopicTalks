﻿using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Controllers;

[Route("File")]
public class FileController(IHttpService httpService) : Controller
{
    private readonly IHttpService _httpService = httpService;

    public IActionResult Index()
    {
        return View();
    }

    [Route("Form")]
    public IActionResult Form()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        using (var content = new MultipartFormDataContent())
        {
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

            var response = await _httpService.Client.PostAsync("api/cloud", content);

            if (response.IsSuccessStatusCode)
            {
                var x = response.ToJson();
                return Ok(x);
            }

            return StatusCode((int)response.StatusCode, "Error uploading file.");
        }
    }

}