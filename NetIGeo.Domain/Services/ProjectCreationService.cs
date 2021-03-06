﻿using AutoMapper;

using NetIGeo.DataAccess.Common;
using NetIGeo.DataAccess.Documents;
using NetIGeo.DataAccess.RavenDb;
using NetIGeo.Domain.Models;

namespace NetIGeo.Domain.Services
{
    public interface IProjectCreationService
    {
        ServiceResult<ProjectModel> Create(ProjectModel project);
    }

    public class ProjectCreationService : IProjectCreationService
    {
        private readonly IMapper _mapper;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IServiceResultCreator _serviceResultCreator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ProjectCreationService(IProjectDocumentRepository projectDocumentRepository,
                                      IMapper mapper,
                                      IServiceResultCreator serviceResultCreator,
                                      IDateTimeProvider dateTimeProvider)
        {
            _projectDocumentRepository = projectDocumentRepository;
            _mapper = mapper;
            _serviceResultCreator = serviceResultCreator;
            _dateTimeProvider = dateTimeProvider;
        }

        public ServiceResult<ProjectModel> Create(ProjectModel project)
        {
            project.CreateDate = _dateTimeProvider.NowUTC();
            Result<ProjectDocument> storerResult =
                _projectDocumentRepository.Create(_mapper.Map<ProjectDocument>(project));
            return _serviceResultCreator.Create(_mapper.Map<ProjectModel>(storerResult.Contents), storerResult.Success);
        }
    }
}