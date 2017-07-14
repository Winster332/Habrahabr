using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Box2DX.Dynamics;
using Box2DX.Common;
using Box2DX.Collision;
using System.Windows.Media.Media3D;

namespace Box2D_and_WPF
{
	public class Physics
	{
		private World world;
		private const string PATH_CIRCLE = @"Assets\circle.png"; // Изображение круга
		private const string PATH_RECT = @"Assets\rect.png"; // Изображение квадрата
		private Model3DGroup models;

		public Physics(float x, float y, float w, float h, float g_x, float g_y, bool doSleep)
		{
			AABB aabb = new AABB();
			aabb.LowerBound.Set(x, y); // Указываем левый верхний угол начала границ
			aabb.UpperBound.Set(w, h); // Указываем нижний правый угол конца границ
			Vec2 g = new Vec2(g_x, g_y); // Устанавливаеи вектор гравитации
			world = new World(aabb, g, doSleep); // Создаем мир
		}

		public void SetModelsGroup(Model3DGroup models)
		{
			this.models = models;
		}

		public MyModel3D AddBox(float x, float y, float w, float h, float density, float friction, float restetution)
		{
			// Создается наша графическая модель
			MyModel3D model = new MyModel3D(models, x, -y, 0, Environment.CurrentDirectory + "\\" + PATH_RECT, new System.Windows.Size(w, h));
			// Необходим для установи позиции, поворота, различных состояний и т.д. Советую поюзать свойства этих объектов
			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x+w, y+h);
			bDef.Angle = 0;
			// Наш полигон который описывает вершины			
			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(w / 2, h / 2);
			// Создание самого тела
			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();
			body.SetUserData(model); // Это отличная функция, она на вход принемает объекты типа object, я ее использовал для того чтобы запихнуть и хранить в ней нашу графическую модель, и в методе step ее доставать и обновлять
			return model;
		}
		public MyModel3D AddCircle(float x, float y, float radius, float angle, float density,
					float friction, float restetution)
		{
			MyModel3D model = new MyModel3D(models, x, -y, 0, Environment.CurrentDirectory + "\\" + PATH_CIRCLE, new System.Windows.Size(radius * 2, radius * 2));

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x, y);
			bDef.Angle = angle;

			CircleDef pDef = new CircleDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.Radius = radius;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			body.SetUserData(model);

			return model;
		}
		public void Step(float dt, int iterat)
		{
			// Параметры этого метода управляют временем мира и точностью обработки коллизий тел
			world.Step(dt / 1000.0f, iterat, iterat);

			for (Body list = world.GetBodyList(); list != null; list = list.GetNext())
			{
				if (list.GetUserData() != null)
				{
					System.Windows.Media.Media3D.Vector3D position = new System.Windows.Media.Media3D.Vector3D(
						list.GetPosition().X, list.GetPosition().Y, 0);
					float angle = list.GetAngle() * 180.0f / (float)System.Math.PI; // Выполняем конвертацию из градусов в радианы
					MyModel3D model = (MyModel3D)list.GetUserData();
					model.SetPosition(position); // Перемещаем нашу графическую модель по x,y
					model.Rotation(new System.Windows.Media.Media3D.Vector3D(0, 0, 1), angle); // Вращаем по координате x
				}
			}
		}
	}
}
