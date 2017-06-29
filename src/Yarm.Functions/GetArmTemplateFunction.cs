﻿using System.Net.Http;
using System.Threading.Tasks;

using Yarm.Extensions;
using Yarm.Functions.FunctionFactories;
using Yarm.Services;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the function entity for the trigger.
    /// </summary>
    public class GetArmTemplateFunction : FunctionBase, IGetArmTemplateFunction
    {
        private readonly IGitHubService _gitHubService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateFunction"/> class.
        /// </summary>
        /// <param name="gitHubService"><see cref="IGitHubService"/> instance.</param>
        public GetArmTemplateFunction(IGitHubService gitHubService)
        {
            this._gitHubService = gitHubService.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets or sets the template directory name.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public override async Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            if (this.TemplateName.IsNullOrWhiteSpace())
            {
                this.LogWarning(ResponseMessages.TemplateNotFound);

                return this.CreateNotFoundResponse(req, ResponseMessages.TemplateNotFound);
            }

            var template = await this._gitHubService.GetArmTemplateAsync(this.TemplateName).ConfigureAwait(false);
            var json = await this._gitHubService.GetArmTemplateAsJsonAsync(template.DownloadUrl).ConfigureAwait(false);

            return this.CreateOkResponse(req, json, "text/plain");
        }
    }
}