using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using eXpand.ExpressApp.ModelArtifactState.ActionState;
using eXpand.ExpressApp.ModelArtifactState.Interfaces;
using eXpand.ExpressApp.ModelArtifactState.StateInfos;
using eXpand.Persistent.Base.General;

namespace eXpand.ExpressApp.ModelArtifactState.Controllers{
    public partial class ActionStateCustomizationViewController : ViewController, ISupportArtifactStateVisibilityCustomization, ISupportArtifactStateAccessibilityCustomization{
        readonly Dictionary<ActionBase,ActionState.ActionState> actionsToExecute = new Dictionary<ActionBase, ActionState.ActionState>();
        public ActionStateCustomizationViewController(){
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnFrameAssigned(){
            base.OnFrameAssigned();
            var controller = Frame.GetController<ArtifactStateCustomizationViewController>();
            controller.ArtifactStateCustomizing+=ControllerOnArtifactStateCustomizing;
            controller.Register<ActionStateRule>(this);
        }

        void ControllerOnArtifactStateCustomizing(object sender, ArtifactStateInfoCustomizingEventArgs artifactStateInfoCustomizingEventArgs) {
            ArtifactStateInfo artifactStateInfo = artifactStateInfoCustomizingEventArgs.ArtifactStateInfo;
            bool executedAndDisable = artifactStateInfo.State == (State)ActionState.ActionState.ExecutedAndDisable;
            bool b = artifactStateInfo.State == (State)ActionState.ActionState.Executed || executedAndDisable;

            if (b && actionsToExecute.Count==0)            {
//                _executed = true;
                var rule = (((ActionStateRule)artifactStateInfo.Rule));
                foreach (SimpleAction actionBase in GetActions(rule)) {
                    actionsToExecute.Add(actionBase,(ActionState.ActionState) artifactStateInfo.State);
//                    var action = actionBase;
//                    ViewControlsCreated+=(o, args) => ExecuteAction(action, executedAndDisable);
//                    var viewController = ((ViewController) actionBase.Controller);
//                    viewController.ViewControlsCreated +=(sender1, eventArgs) => ExecuteAction(action,executedAndDisable);
                }
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            foreach (var keyValuePair in actionsToExecute) {
                ExecuteAction((SimpleAction)keyValuePair.Key,keyValuePair.Value==ActionState.ActionState.ExecutedAndDisable);
            }
            actionsToExecute.Clear();
        }
        void ExecuteAction(SimpleAction action, bool executedAndDisable) {
            action.Active[ArtifactStateCustomizationViewController.ActiveObjectTypeHasRules] = true;
            if (action.Active){
                action.DoExecute();    
            }
            if (executedAndDisable)
                action.Active[ArtifactStateCustomizationViewController.ActiveObjectTypeHasRules] = false;
        }


        public void CustomizeVisibility(ArtifactStateInfo artifactStateInfo){
            var rule = (((ActionStateRule)artifactStateInfo.Rule));
            foreach (ActionBase actionBase in GetActions(rule)){
                actionBase.Active[ArtifactStateCustomizationViewController.ActiveObjectTypeHasRules] =
                    !artifactStateInfo.Active;
            }
        }
        private IEnumerable<ActionBase> GetActions(ActionStateRule rule){
            IEnumerable<ActionBase> actionBases =
                Frame.Controllers.Cast<Controller>().SelectMany(controller => controller.Actions);
            if (!string.IsNullOrEmpty(rule.Module)){
                return GetModuleActions(rule, actionBases);
            }
            ActionBase actionBase =actionBases.Where(@base => @base.Id == rule.ActionId).Select(@base => @base).Single();
            return new List<ActionBase>{actionBase};
        }

        IEnumerable<ActionBase> GetModuleActions(ActionStateRule rule, IEnumerable<ActionBase> actionBases) {
            IEnumerable<string> assemblies =
                Application.Modules.Where(@base => new Regex(rule.Module).IsMatch(@base.GetType().FullName)).Select(
                    @base => @base.GetType().Assembly.FullName);
            return actionBases.Where(@base => assemblies.Contains(@base.Controller.GetType().Assembly.FullName));
        }

        public void CustomizeAccessibility(ArtifactStateInfo artifactStateInfo){
            var rule = (((ActionStateRule)artifactStateInfo.Rule));
            foreach (ActionBase actionBase in GetActions(rule)){
                actionBase.Enabled[ArtifactStateCustomizationViewController.ActiveObjectTypeHasRules] =
                    !artifactStateInfo.Active;
            }
        }
    }
}