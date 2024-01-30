using System.Collections.Generic;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    public class SolutionDocumentationContent
    {
        public List<FlowEntity> flows = new List<FlowEntity>();
        public List<AppEntity> apps = new List<AppEntity>();
        public SolutionEntity solution;
        public string folderPath, filename;

        public SolutionDocumentationContent(SolutionEntity solution, List<AppEntity> apps, List<FlowEntity> flows, string path)
        {
            this.solution = solution;
            this.apps = apps;
            this.flows = flows;
            filename = CharsetHelper.GetSafeName(solution.UniqueName);
            folderPath = path;
        }
    }
}