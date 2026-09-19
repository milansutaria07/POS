using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rajani_ERP
{
    // Add this file to the same project as MainPage.Designer.cs.
    // It uses the existing controls from your designer and does not require
    // rebuilding the UI in the Visual Studio designer.
    public partial class MainPage : Form
    {
        private bool _navCollapsed;
        private const int DesktopNavWidth = 262;
        private const int MobileNavWidth = 68;
        private const int HeaderHeight = 60;
        private const int MobileBreakpoint = 850;

        public MainPage()
        {
            InitializeComponent();
            EnableResponsiveLayout();
        }


        private void EnableResponsiveLayout()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(620, 500);
            MaximizeBox = true;

            Resize += MainPage_Resize;

            // Make the shell controlled by code instead of fixed designer
            // coordinates.
            pnl_main.Dock = DockStyle.Fill;

            // Prevent the designer's fixed locations from fighting the
            // responsive layout.
            pnl_nav_bar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                            AnchorStyles.Left | AnchorStyles.Right;

            // These pages should always occupy the available content area.
            pg_main.Dock = DockStyle.Fill;
            pg_cash_sale_page.Dock = DockStyle.Fill;

            ApplyResponsiveLayout();
        }

        private void MainPage_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (pnl_main == null || pnl_nav_bar == null || panel1 == null || panel2 == null)
                return;

            int width = pnl_main.ClientSize.Width;
            int height = pnl_main.ClientSize.Height;

            if (width <= 0 || height <= 0)
                return;

            bool mobile = width < MobileBreakpoint;

            // Collapse the navigation on narrow screens.
            SetNavigationMode(mobile);

            int navWidth = _navCollapsed ? MobileNavWidth : DesktopNavWidth;
            navWidth = Math.Min(navWidth, Math.Max(0, width - 300));

            // Explicit bounds are used here because the original designer
            // contains fixed coordinates such as Location = (262, 60).
            pnl_nav_bar.SetBounds(0, 0, navWidth, height);

            int contentWidth = Math.Max(0, width - navWidth);

            panel1.SetBounds(navWidth, 0, contentWidth, HeaderHeight);
            panel2.SetBounds(navWidth, HeaderHeight,
                             contentWidth,
                             Math.Max(0, height - HeaderHeight));

            ResizeHeader(contentWidth, mobile);
            ResizeCashSalePages(contentWidth, panel2.ClientSize.Height, mobile);

            pnl_main.Invalidate();
        }

        private void SetNavigationMode(bool mobile)
        {
            if (_navCollapsed == mobile)
            {
                // Still refresh button sizing when the form is resized.
                UpdateNavigationControls(_navCollapsed);
                return;
            }

            _navCollapsed = mobile;
            UpdateNavigationControls(_navCollapsed);
        }

        private void UpdateNavigationControls(bool collapsed)
        {
            int navWidth = collapsed ? MobileNavWidth : DesktopNavWidth;

            pnl_nav_bar.Width = navWidth;
            pnl_logo.Width = navWidth;
            pnl_nav_buttons.Width = navWidth;
            pnl_spacer.Width = navWidth;

            // Keep the logo visible, but remove the text when collapsed.
            label1.Visible = !collapsed;
            label2.Visible = !collapsed;

            if (collapsed)
            {
                pic_logo.SetBounds(9, 9, 50, 50);
            }
            else
            {
                pic_logo.SetBounds(15, 9, 70, 63);
            }

            // Existing Bunifu buttons are docked vertically in the
            // designer, so they automatically fill the available width.
            foreach (Bunifu.UI.WinForms.BunifuButton.BunifuButton button in
                     new Bunifu.UI.WinForms.BunifuButton.BunifuButton[]
                     {
                         cmd_dashboard, cmd_sales, cmd_dn, cmd_invoices,
                         cmd_stock, cmd_products, cmd_settings
                     })
            {
                button.Width = navWidth;
                button.Height = collapsed ? 50 : 37;

                if (collapsed)
                {
                    button.Text = "";
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.TextMarginLeft = 0;
                    button.IconMarginLeft = 0;
                    button.IconPadding = 8;
                    button.Padding = new Padding(0);
                }
                else
                {
                    RestoreNavigationButtonText(button);
                    button.TextAlign = ContentAlignment.MiddleLeft;
                    button.IconMarginLeft = 20;
                    button.IconPadding = 10;
                }
            }

            pnl_nav_buttons.PerformLayout();
        }

        private void RestoreNavigationButtonText(
            Bunifu.UI.WinForms.BunifuButton.BunifuButton button)
        {
            if (button == cmd_dashboard)
            {
                button.Text = "Dashboard";
                button.TextMarginLeft = -40;
            }
            else if (button == cmd_sales)
            {
                button.Text = "Sales";
                button.TextMarginLeft = -58;
            }
            else if (button == cmd_dn)
            {
                button.Text = "Delivery Note";
                button.TextMarginLeft = -28;
            }
            else if (button == cmd_invoices)
            {
                button.Text = "Invoices";
                button.TextMarginLeft = -47;
            }
            else if (button == cmd_stock)
            {
                button.Text = "Stock";
                button.TextMarginLeft = -56;
            }
            else if (button == cmd_products)
            {
                button.Text = "Products";
                button.TextMarginLeft = -45;
            }
            else if (button == cmd_settings)
            {
                button.Text = "Settings";
                button.TextMarginLeft = -48;
            }
        }

        private void ResizeHeader(int contentWidth, bool mobile)
        {
            if (panel1 == null)
                return;

            // Breadcrumb stays on the left.
            label11.Location = new Point(mobile ? 10 : 11, 27);

            // User/profile controls stay on the right.
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bunifuImageButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            int right = Math.Max(10, contentWidth - 10);

            comboBox1.SetBounds(
                Math.Max(10, right - comboBox1.Width),
                19,
                Math.Min(comboBox1.Width, Math.Max(100, contentWidth - 20)),
                25);

            bunifuImageButton1.SetBounds(
                Math.Max(10, comboBox1.Left - 52),
                10, 40, 40);

            pictureBox2.SetBounds(
                Math.Max(10, bunifuImageButton1.Left - 40),
                15, 30, 30);

            // Search becomes too crowded on small screens.
            bool showSearch = contentWidth >= 650 && !mobile;
            label10.Visible = showSearch;

            if (showSearch)
            {
                int searchRight = pictureBox2.Left - 15;
                int searchWidth = Math.Max(120, searchRight - 350);

                label10.SetBounds(
                    Math.Max(190, searchRight - searchWidth),
                    18,
                    searchWidth,
                    25);
            }
        }

        private void ResizeCashSalePages(
            int contentWidth,
            int contentHeight,
            bool mobile)
        {
            if (tabPage8 != null)
                ResizeCashSaleDashboard(contentWidth, contentHeight, mobile);

        }

        private void ResizeCashSaleDashboard(
            int width,
            int height,
            bool mobile)
        {
            if (panel3 == null || tableLayoutPanel1 == null || bunifuDataGridView2 == null)
                return;

            // Top filter/action area.
            panel3.SetBounds(0, 0, Math.Max(0, width), mobile ? 150 : 100);

            label12.Location = new Point(10, 14);
            bunifuDatepicker1.SetBounds(58, 8, 172, 30);

            int buttonWidth = mobile
                ? Math.Max(110, (width - 30) / 3)
                : 125;

            if (mobile)
            {
                bunifuButton1.SetBounds(10, 50, buttonWidth, 40);
                bunifuButton2.SetBounds(20 + buttonWidth, 50, buttonWidth, 40);
                bunifuButton3.SetBounds(30 + buttonWidth * 2, 50, buttonWidth, 40);
            }
            else
            {
                bunifuButton1.SetBounds(Math.Max(10, width - 424), 47, 125, 40);
                bunifuButton2.SetBounds(Math.Max(10, width - 282), 47, 125, 40);
                bunifuButton3.SetBounds(Math.Max(10, width - 141), 47, 125, 40);
            }

            // Search field should use the available width.
            label13.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            int searchWidth = mobile
                ? Math.Max(100, width - 24)
                : Math.Min(500, Math.Max(200, width - 24));

            label13.SetBounds(12, mobile ? 100 : 58, searchWidth, 25);

            // Recent-sales table fills the remaining area.
            int top = mobile ? 160 : 100;

            tableLayoutPanel1.SetBounds(
                3,
                top,
                Math.Max(0, width - 6),
                Math.Max(80, height - top - 3));

            tableLayoutPanel1.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom |
                AnchorStyles.Left | AnchorStyles.Right;

            label19.Dock = DockStyle.Top;

            bunifuDataGridView2.Dock = DockStyle.Fill;
        }

        // Keep your existing business logic in these handlers if you already
        // have implementations in your project. These default implementations
        // make this pair compile and provide basic page navigation.
        private void Cmd_dashboard_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 0;
        }

        private void Cmd_sales_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 1;
        }

        private void Cmd_dn_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 2;
        }

        private void Cmd_invoices_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 3;
        }

        private void Cmd_stock_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 4;
        }

        private void Cmd_products_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 5;
        }

        private void Cmd_settings_Click(object sender, EventArgs e)
        {
            pg_main.SelectedIndex = 6;
        }

        private void BunifuButton1_Click(object sender, EventArgs e)
        {
            // Create Cash Sale
            PopUpForm PopUpForm = new PopUpForm();
            PopUpForm.Show();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Intentionally empty. Put your existing logout/close logic here
            // if your application already uses it.
        }

        private void BunifuButton2_Click_1(object sender, EventArgs e)
        {
            PopUpForm PopUpForm = new PopUpForm();
            PopUpForm.Show();
        }
    }
}
