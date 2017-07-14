using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Box2D_and_WPF
{
	public class MyModel3D
	{
		public Vector3D Position { get; set; } // Позиция квадрата
		public Size Size { get; set; } // Размер квадрата
		private TranslateTransform3D translateTransform; // Матрица перемещения
		private RotateTransform3D rotationTransform; // Матрица вращения
		public MyModel3D(Model3DGroup models, double x, double y, double z, string path, Size size, float axis_x = 0, double angle = 0, float axis_y = 0, float axis_z = 1)
		{
			this.Size = size;
			this.Position = new Vector3D(x, y, z);
			MeshGeometry3D mesh = new MeshGeometry3D();
			mesh.Positions = new Point3DCollection(new List<Point3D>
			{
				new Point3D(-size.Width/2, -size.Height/2, 0),
				new Point3D(size.Width/2, -size.Height/2, 0),
				new Point3D(size.Width/2, size.Height/2, 0),
				new Point3D(-size.Width/2, size.Height/2, 0)
			});
			mesh.TriangleIndices = new Int32Collection(new List<int> { 0, 1, 2, 0, 2, 3 });
			mesh.TextureCoordinates = new PointCollection();
			mesh.TextureCoordinates.Add(new Point(0, 1));
			mesh.TextureCoordinates.Add(new Point(1, 1));
			mesh.TextureCoordinates.Add(new Point(1, 0));
			mesh.TextureCoordinates.Add(new Point(0, 0));

			ImageBrush brush = new ImageBrush(new BitmapImage(new Uri(path)));
			Material material = new DiffuseMaterial(brush);
			GeometryModel3D geometryModel = new GeometryModel3D(mesh, material);
			models.Children.Add(geometryModel);
			translateTransform = new TranslateTransform3D(x, y, z);
			rotationTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(axis_x, axis_y, axis_z), angle), 0.5, 0.5, 0.5);

			Transform3DGroup tgroup = new Transform3DGroup();
			tgroup.Children.Add(translateTransform);
			tgroup.Children.Add(rotationTransform);
			geometryModel.Transform = tgroup;
		}
		public void SetPosition(Vector3D v3)
		{
			translateTransform.OffsetX = v3.X;
			translateTransform.OffsetY = v3.Y;
			translateTransform.OffsetZ = v3.Z;
		}
		public Vector3D GetPosition()
		{
			return new Vector3D(translateTransform.OffsetX, translateTransform.OffsetY, translateTransform.OffsetZ);
		}
		public void Rotation(Vector3D axis, double angle, double centerX = 0.5, double centerY = 0.5, double centerZ = 0.5)
		{
			rotationTransform.CenterX = translateTransform.OffsetX;
			rotationTransform.CenterY = translateTransform.OffsetY;
			rotationTransform.CenterZ = translateTransform.OffsetZ;

			rotationTransform.Rotation = new AxisAngleRotation3D(axis, angle);
		}
		public Size GetSize()
		{
			return Size;
		}
	}
}
