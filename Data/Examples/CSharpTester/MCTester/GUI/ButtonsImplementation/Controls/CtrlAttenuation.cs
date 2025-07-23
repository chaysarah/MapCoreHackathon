using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MapCore;

namespace MCTester.Controls
{
	public partial class CtrlAttenuation : UserControl
	{
		public CtrlAttenuation()
		{
			InitializeComponent();
		}

		public float Const
		{
			get { return ntxConstValue.GetFloat(); }
			set { ntxConstValue.SetFloat(value); }
		}

		public float Linear
		{
			get { return ntxLinearValue.GetFloat(); }
            set { ntxLinearValue.SetFloat(value); }
		}

		public float Square
		{
			get { return ntxSquareValue.GetFloat(); }
            set { ntxSquareValue.SetFloat(value); }
		}

		public float Range
		{
			get { return ntxRange.GetFloat(); }
			set { ntxRange.SetFloat(value); }
		}

		public DNSMcAttenuation Attenuation
		{
			get
			{
				return new DNSMcAttenuation(Const, Linear, Square, Range);
			}
			set
			{
				Const = value.fConst;
				Linear = value.fLinear;
				Square = value.fSquare;
				Range = value.fRange;
			}
		}
	}
}
