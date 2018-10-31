using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinger.Abstract
{
	/// <summary>
	/// Attempt at isolating document-level actions like prompting for save on close, detecting dirty status, but not used
	/// </summary>
	internal abstract class DocumentManager
	{
		private bool _isModified = false;

		public event EventHandler ModifiedChanged;

		public bool IsModified
		{
			get { return _isModified; }
			set
			{				
				if (value != _isModified)
				{
					_isModified = value;
					ModifiedChanged?.Invoke(this, new EventArgs());
				}
			}
		}

		protected abstract void SaveDocument(string fileName);

		public void Save(string fileName)
		{
			SaveDocument(fileName);
			IsModified = false;
		}
	}
}
