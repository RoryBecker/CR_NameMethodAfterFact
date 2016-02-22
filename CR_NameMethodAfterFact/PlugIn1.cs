using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor;
using System.Text.RegularExpressions;

namespace CR_NameMethodAfterFact
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            registerNameMethodAfterFact();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {

            base.FinalizePlugIn();
        }
        #endregion

        public void registerNameMethodAfterFact()
        {
            DevExpress.Refactor.Core.RefactoringProvider NameMethodAfterFact = new DevExpress.Refactor.Core.RefactoringProvider(components);
            ((System.ComponentModel.ISupportInitialize)(NameMethodAfterFact)).BeginInit();
            NameMethodAfterFact.ProviderName = "NameMethodAfterFact"; // Should be Unique
            NameMethodAfterFact.DisplayName = "Name Method After Fact";
            NameMethodAfterFact.CheckAvailability += NameMethodAfterFact_CheckAvailability;
            NameMethodAfterFact.Apply += NameMethodAfterFact_Execute;
            ((System.ComponentModel.ISupportInitialize)(NameMethodAfterFact)).EndInit();
        }

        AssignmentExpression DisplayNameArg;

        private void NameMethodAfterFact_CheckAvailability(Object sender, CheckContentAvailabilityEventArgs ea)
        {
            DisplayNameArg = null;
            
            // Caret in Method signature
            Method Method = ea.Element as Method;
            if (Method == null)
                return;

            // Method Has Attribute called Fact
            var AttributeFact = Method.FindAttribute("Fact");
            if (AttributeFact == null)
                return;

            // Locate attribute called Displayname
            DisplayNameArg = GetTheArg(AttributeFact);
            if (DisplayNameArg == null)
                return;

            ea.Available = true; 
        }
        private static AssignmentExpression GetTheArg(DevExpress.CodeRush.StructuralParser.Attribute AttributeFact)
        {
            AssignmentExpression TheArg = null;
            ExpressionCollection factArguments = AttributeFact.Arguments;
            AssignmentExpression Arg = factArguments[0] as AssignmentExpression;

            if (Arg.LeftSide.Name.ToLower() == "DisplayName".ToLower())
            {
                TheArg = Arg;
            }
            return TheArg;
        }
        private void NameMethodAfterFact_Execute(Object sender, ApplyContentEventArgs ea)
        {
            // This method is executed when the system executes your refactoring 
            string NewName = Regex.Replace(DisplayNameArg.RightSide.Name.RemoveQuotes(), "[^A-Za-z0-9]", "_");
            ea.TextDocument.QueueReplace((ea.Element as Method).NameRange, NewName);
            ea.TextDocument.ApplyQueuedEdits("Renamed Fact Method to Reflect DisplayName");
        }
    }

    public static class stringext
    {
        public static string RemoveQuotes(this string Source)
        {
            return Source.Substring(1, Source.Length - 2);
        }
    }
}