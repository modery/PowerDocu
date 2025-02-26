using System;
using System.Collections.Generic;
using System.Linq;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    public class SolutionDocumentationContent
    {
        public List<FlowEntity> flows = new List<FlowEntity>();
        public List<AppEntity> apps = new List<AppEntity>();
        public SolutionEntity solution;
        public string folderPath,
            filename;

        public SolutionDocumentationContent(
            SolutionEntity solution,
            List<AppEntity> apps,
            List<FlowEntity> flows,
            string path
        )
        {
            this.solution = solution;
            this.apps = apps;
            this.flows = flows;
            filename = CharsetHelper.GetSafeName(solution.UniqueName);
            folderPath = path;
        }

        public string GetDisplayNameForComponent(SolutionComponent component)
        {
            if (component.Type == "Workflow")
            {
                FlowEntity flow = flows.Where(f => f.Name.Contains(component.ID, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
                if (flow != null)
                {
                    return flow.Name + " (" + flow.trigger.Name + ": " + flow.trigger.Type + ")";
                }
            }
            return solution.GetDisplayNameForComponent(component);
        }
    }
}
