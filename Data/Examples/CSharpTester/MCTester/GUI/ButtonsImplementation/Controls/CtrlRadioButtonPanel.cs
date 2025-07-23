using System;
using System.Windows.Forms;

namespace MCTester.Controls
{
    /// <summary>
    /// This class oversees the checkin/unchecking when mixing RadioButton objects with RadioGroupBox objects within the same container.
    /// </summary>
    public class CtrlRadioButtonPanel : Panel
    {
        /// <summary>
        /// RadioButtonPanel public constructor.
        /// </summary>
        public CtrlRadioButtonPanel()
        {
        }

        /// <summary>
        /// Hooks Check callback events of RadioButton objects within the panel to the RadioButtonPanel object.
        /// </summary>
        public void AddCheckEventListeners()
        {
            foreach (Control control in this.Controls)
            {
                RadioButton radioButton = control as RadioButton;
                if (radioButton != null)
                {
                    radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                }
                else
                {
                    CtrlRadioGroupBox radioGroupBox = control as CtrlRadioGroupBox;
                    if (radioGroupBox != null)
                    {
                        radioGroupBox.CheckedChanged += new EventHandler(radioGroupBox_CheckedChanged);
                    }
                }
            }
        }

        /// <summary>
        /// Event callback called when a RadioButton's Check property is changed.
        /// </summary>
        /// <param name="sender">Object(RadioButton)</param>
        /// <param name="e">EventArgs</param>
        public void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonClick((Control)sender);
        }

        /// <summary>
        /// Event callback called when a RadioGroupBox's Check property is changed.
        /// </summary>
        /// <param name="sender">Object(RadioGroupBox)</param>
        /// <param name="e">EventArgs</param>
        public void radioGroupBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleRadioButtonClick((Control)sender);
        }

        private void HandleRadioButtonClick(Control clickedControl)
        {
            if (clickedControl == null)
                return;

            if (clickedControl.Parent is CtrlRadioGroupBox)
            {
                // If a RadioGroupBox is checked, the sender is the RadioButton,
                // not the RadioGroupBox, but we need the RadioGroupBox object.
                clickedControl = clickedControl.Parent;
            }

            RadioButton clickedRadioButton = clickedControl as RadioButton;
            if (clickedRadioButton != null)
            {
                if (clickedRadioButton.Checked != true)
                {
                    // Only respond to check events that pertain to the control being checked on
                    return;
                }
            }
            else
            {
                CtrlRadioGroupBox clickedRadioGroupBox = clickedControl as CtrlRadioGroupBox;
                if (clickedRadioGroupBox != null)
                {
                    if (clickedRadioGroupBox.Checked != true)
                    {
                        // Only respond to check events that pertain to the control being checked on
                        return;
                    }
                }
            }

            foreach (Control control in this.Controls)
            {
                if (control != clickedControl)
                {
                    RadioButton radioButton = control as RadioButton;
                    if (radioButton != null)
                    {
                        // Normally .NET and WinForms would take care of this but
                        // we need a mechanism that turns off radio buttons if a
                        // radio group box is checked.
                        if (radioButton.Checked != false)
                            radioButton.Checked = false;
                    }
                    else
                    {
                        CtrlRadioGroupBox radioGroupBox = control as CtrlRadioGroupBox;
                        if (radioGroupBox != null)
                        {
                            radioGroupBox.Checked = false;
                        }
                        else
                        {
                            // Not expected... must be some other kind of control.
                        }
                    }
                }
            }
        }
    }
}
