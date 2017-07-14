using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Box2D_and_WPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Physics px;
		public MainWindow()
		{
			InitializeComponent();

			px = new Physics(-1000, -1000, 1000, 1000, 0, -0.005f, false);
			px.SetModelsGroup(models);
			px.AddBox(0.6f, -2, 1, 1, 0, 0.3f, 0.2f);
			px.AddBox(0, 0, 1, 1, 0.5f, 0.3f, 0.2f);

			this.LayoutUpdated += MainWindow_LayoutUpdated;
		}
		private void MainWindow_LayoutUpdated(object sender, EventArgs e)
		{
			px.Step(1.0f, 20); // тут по хорошему нужно вычислять дельту времени, но мне лень :)
			this.InvalidateArrange();
		}
	}
}
