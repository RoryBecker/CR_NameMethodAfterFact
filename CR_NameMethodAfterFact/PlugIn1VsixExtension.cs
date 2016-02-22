using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_NameMethodAfterFact
{
    [Export(typeof(IVsixPluginExtension))]
    public class CR_NameMethodAfterFactExtension : IVsixPluginExtension { }
}