using System;
using System.Collections.Generic;
using System.Drawing;
using GoProject.Properties;

namespace GoProject.Nodes
{
    public class SubProcessNode : GroupNode
    {
        #region Properties

        public new NodeCategory Category { get; }

        public new GatewayType? GatewayType { get; }

        public new EventType? EventType { get; }

        public new EventDimension? EventDimension { get; }

        public new TaskType? TaskType { get; }

        public new bool? IsSubProcess { get; }
        
        public new List<object> BoundaryEventArray { get; }

        public new string Color { get; }

        public new Color? HexColor { get; }

        public new string Size { get; }

        public new SizeF? SizeF { get; }

        #endregion

        #region Constructs

        public SubProcessNode()
        {
            Key = $"subProcess_{Guid.NewGuid()}";
            Text = Localization.Subprocess;
            Category = NodeCategory.subprocess;

            EventType = null;
            EventDimension = null;
            GatewayType = null;
            TaskType = null;
            IsSubProcess = true;
            BoundaryEventArray = null;
            Color = null;
            HexColor = null;
            Size = null;
            SizeF = null;
        }

        #endregion


    }
}