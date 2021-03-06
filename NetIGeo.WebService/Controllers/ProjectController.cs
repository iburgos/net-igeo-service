﻿using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using NetIGeo.Domain.Models;
using NetIGeo.Domain.Services;
using NetIGeo.WebService.Dtos;

namespace NetIGeo.WebService.Controllers
{
    [RoutePrefix("igeoservice")]
    public class ProjectController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IProjectCreationService _projectCreationService;
        private readonly IProjectDeleteService _projectDeleteService;
        private readonly IProjectRetrieverService _projectRetrieverService;
        private readonly IProjectUpdateService _projectUpdateService;

        public ProjectController(IProjectCreationService projectCreationService,
            IProjectUpdateService projectUpdateService,
            IProjectRetrieverService projectRetrieverService,
            IProjectDeleteService projectDeleteService,
            IMapper mapper)
        {
            _projectCreationService = projectCreationService;
            _projectUpdateService = projectUpdateService;
            _projectRetrieverService = projectRetrieverService;
            _projectDeleteService = projectDeleteService;
            _mapper = mapper;
        }

        [HttpGet, Route("project/all")]
        public IHttpActionResult Get()
        {
            IHttpActionResult result = InternalServerError();

            var serviceResult = _projectRetrieverService.Get();
            if (serviceResult.Success)
                result = Ok(_mapper.Map<IEnumerable<ProjectDto>>(serviceResult.Contents));

            return result;
        }

        [HttpGet, Route("project/{id}")]
        public IHttpActionResult Get(int id)
        {
            IHttpActionResult result = InternalServerError();

            var serviceResult = _projectRetrieverService.Get(id);
            if (serviceResult.Success)
                result = Ok(_mapper.Map<ProjectDto>(serviceResult.Contents));
            else
                result = NotFound();

            return result;
        }

        [HttpPost, Route("project/create")]
        public IHttpActionResult New([FromBody] ProjectDto project)
        {
            IHttpActionResult result = InternalServerError();

            if (project != null)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);
                var serviceResult = _projectCreationService.Create(projectModel);
                if (serviceResult.Success)
                    result = Ok(_mapper.Map<ProjectDto>(serviceResult.Contents));
            }
            else
                result = BadRequest();

            return result;
        }

        [HttpPost, Route("project/update")]
        public IHttpActionResult Update([FromBody] ProjectDto project)
        {
            IHttpActionResult result = InternalServerError();

            if (project != null)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);
                var serviceResult = _projectUpdateService.Update(projectModel);
                if (serviceResult.Success)
                    result = Ok(_mapper.Map<ProjectDto>(serviceResult.Contents));
            }
            else
                result = BadRequest();

            return result;
        }

        [HttpDelete, Route("project/{id}")]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult result = InternalServerError();
            if (_projectDeleteService.Delete(id))
                result = Ok();
            else
                result = BadRequest();
            
            return result;
        }
    }
}