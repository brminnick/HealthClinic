using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using HealthClinic.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(ViewCellCustomRenderer))]
namespace HealthClinic.iOS
{
	public class ViewCellCustomRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}
	}
}
