using MapCore;
using System;
using System.Windows.Forms;

namespace MCTester.Controls
{
	public class NumericTextBox : TextBox 
	{
		private string m_lastValue = "";
		private const char ENTER_KEY = '\r';
		public event EventHandler EnterKeyPress;
		public event EventHandler OnTextChangedEvent;

		public NumericTextBox()
		{
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnKeyPress);
			this.TextChanged += new EventHandler(OnTextChanged);
			this.LostFocus  += new EventHandler(OnLostFocus);
			this.m_lastValue = this.Text;
		}

		#region Private Methods

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == ENTER_KEY && EnterKeyPress != null)
				EnterKeyPress(this, new EventArgs());
		}

		private void OnLostFocus(object sender, EventArgs e)
		{
			NumericTextBox btb = (NumericTextBox)sender;

			if (btb.Text == "")
				btb.Text = "0";
		}

		private void OnTextChanged(object sender, EventArgs e)
		{
			NumericTextBox btb = (NumericTextBox)sender;
			
			try
			{
				btb.m_lastValue = btb.Text;
			}
			catch
			{
				btb.Text = btb.m_lastValue;
			}

            if(OnTextChangedEvent != null)
                OnTextChangedEvent(this, new EventArgs());
		}
		#endregion

		#region Public methods

        #region Getters
        public int GetInt32()
		{
			try
			{
                int uParam;
                if (String.Compare(Text, "MAX", true) == 0)
                    uParam = int.MaxValue;
                else if (String.Compare(Text, "MAX-1", true) == 0)
                    uParam = int.MaxValue - 1;
                else
                    uParam = Convert.ToInt32(Text);

                return uParam;
			}
			catch
			{
				return 0;
			}
		}

        public ulong GetUInt64()
        {
            try
            {
                ulong uParam;
                if (String.Compare(Text, "MAX", true) == 0)
                    uParam = ulong.MaxValue;
                else if (String.Compare(Text, "MAX-1", true) == 0)
                    uParam = ulong.MaxValue-1;
                else
                    uParam = Convert.ToUInt64(Text);

                return uParam;
            }
            catch
            {
                return (ulong)0;
            }
        }


        public double GetDouble()
		{
			try
			{
                double dParam;
                if (String.Compare(this.Text, "MAX", true) == 0)
                    dParam = double.MaxValue;
                else
                    dParam = double.Parse(this.Text);

                return dParam;
			}
			catch
			{
                return 0;            
			}
		}

        public long GetInt64()
        {
            try
            {
                long uParam;
                if (String.Compare(Text, "MAX", true) == 0)
                    uParam = long.MaxValue;
                else if (String.Compare(Text, "MAX-1", true) == 0)
                    uParam = long.MaxValue-1;
                else
                    uParam = long.Parse(Text);

                return uParam;
            }
            catch
            {
                return 0;
            }
        }

        public float GetSingle()
        {
            try
            {
                return Convert.ToSingle(this.Text);
            }
            catch
            {
                return 0;
            }
        }

        public uint GetUInt32()
        {
            try
            {
                uint uParam;
                if (String.Compare(this.Text, "MAX", true) == 0)
                    uParam = DNMcConstants._MC_EMPTY_ID;
                else if (String.Compare(this.Text, "MAX-1", true) == 0)
                    uParam = DNMcConstants._MC_EMPTY_ID -1;
                else
                    uParam = uint.Parse(this.Text);

                return uParam;
            }
            catch
            {
                return (uint)0;
            }
        }

        public byte GetByte()
        {
            try
            {
                return Convert.ToByte(this.Text);
            }
            catch
            {
                return 0;
            }
        }

        public sbyte GetSByte()
        {
            try
            {
                return Convert.ToSByte(this.Text);
            }
            catch
            {
                return 0;
            }
        }

        public float GetFloat()
        {
            try
            {
                float fParam;
                if (String.Compare(this.Text, "MAX", true) == 0)
                    fParam = float.MaxValue;
                else
                    fParam = float.Parse(this.Text);

                return fParam;
            }
            catch
            {
                return 0f;
            }
        }

        public short GetShort()
        {
            try
            {
                return short.Parse(this.Text);
            }
            catch
            {
                return 0;
            }
        }


		#endregion


        #region Setters
        public void SetFloat(float Val)
        {
            if (Val == float.MaxValue)
                this.Text = "MAX";
            else
                this.Text = Val.ToString();
        }

        public void SetUInt32(uint Val)
        {
            if (Val == DNMcConstants._MC_EMPTY_ID)
                this.Text = "MAX";
            else if (Val == DNMcConstants._MC_EMPTY_ID-1)
                this.Text = "MAX-1";
            else
                this.Text = Val.ToString();
        }

        public void SetUInt64(ulong Val)
        {
            if (Val == ulong.MaxValue)
                this.Text = "MAX";
            else if (Val == ulong.MaxValue - 1)
                this.Text = "MAX-1";
            else
                this.Text = Val.ToString();
        }

        public void SetInt(int Val)
        {
            if (Val == int.MaxValue)
                this.Text = "MAX";
            else if (Val == int.MaxValue -1)
                this.Text = "MAX-1";
            else
                this.Text = Val.ToString();
        }

        public void SetDouble(double Val)
        {
            if (Val == double.MaxValue)
                this.Text = "MAX";
            else
            {
                this.Text = Val.ToString("G17");
            }
        }

        public void SetShort(short Val)
        {
            this.Text = Val.ToString();
        }

        public void SetByte(byte Val)
        {
            this.Text = Val.ToString();
        }

        public void SetSByte(sbyte Val)
        {
            this.Text = Val.ToString();
        }

        public void SetString(string Val)
        {
            this.Text = Val;
        }

    	#endregion
        #endregion

    }
}
