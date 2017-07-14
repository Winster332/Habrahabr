using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Box2DX.Dynamics;

namespace Box2D_and_WPF
{
	public class Solver : ContactListener
	{
		public delegate void EventSolver(MyModel3D body1, MyModel3D body2);
		public event EventSolver OnAdd;
		public event EventSolver OnPersist;
		public event EventSolver OnResult;
		public event EventSolver OnRemove;

		public override void Add(ContactPoint point)
		{
			base.Add(point);

			OnAdd?.Invoke((MyModel3D)point.Shape1.GetBody().GetUserData(), (MyModel3D)point.Shape2.GetBody().GetUserData());
		}

		public override void Persist(ContactPoint point)
		{
			base.Persist(point);

			OnPersist?.Invoke((MyModel3D)point.Shape1.GetBody().GetUserData(), (MyModel3D)point.Shape2.GetBody().GetUserData());
		}

		public override void Result(ContactResult point)
		{
			base.Result(point);

			OnResult?.Invoke((MyModel3D)point.Shape1.GetBody().GetUserData(), (MyModel3D)point.Shape2.GetBody().GetUserData());
		}

		public override void Remove(ContactPoint point)
		{
			base.Remove(point);

			OnRemove?.Invoke((MyModel3D)point.Shape1.GetBody().GetUserData(), (MyModel3D)point.Shape2.GetBody().GetUserData());
		}
	}

}
