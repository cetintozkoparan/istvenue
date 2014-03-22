using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProjectReferencesWrapperModel
    {
        public List<ProjectReferences> projectreference { get; set; }
        public List<ProjectReferenceGroup> projectreferencegroup { get; set; }

        public ProjectReferencesWrapperModel(List<ProjectReferenceGroup> projectreferencegroup, List<ProjectReferences> projectreference)
        {
            this.projectreferencegroup = projectreferencegroup;
            this.projectreference = projectreference;
        }
    }
}