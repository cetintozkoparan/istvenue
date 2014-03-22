using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProjectReferencesSubWrapperModel
    {
        public ProjectReferences reference { get; set; }
        public List<ProjectReferenceGroup> projectreferencegroup { get; set; }
        public List<Photo> photos { get; set; }

        public ProjectReferencesSubWrapperModel(List<ProjectReferenceGroup> projectreferencegroup, ProjectReferences reference, List<Photo> photos)
        {
            this.projectreferencegroup = projectreferencegroup;
            this.reference = reference;
            this.photos = photos;
        }
    }
}