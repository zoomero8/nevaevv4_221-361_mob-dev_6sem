using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorMapLogic;

namespace wfaColorBox
{
    public partial class Form1 : Form
    {
        private ColorMapGenerator _generator;
        private MapCell[,] _currentMap; // ������� �����
        private char _correctColorChar; // ������ �����, �������� ������ ������ ����� �� ������� ����
        private Dictionary<char, Color> _charToColorMapping; // ��� �������������� 'R' � System.Drawing.Color.Red

        // ����� ������� ��� �������� ��������� ���� ������
        private Dictionary<char, string> _colorNamesMapping;

        // ����� ������, ������� ��� �� ���� ������� �� ������� ������
        private List<char> _availableColorsForCurrentRound;

        // ������� ��� �������� ������ �� ������ �� �� �����, ��� �������� ����������
        // �� ��� ����� ����� ������������ ���� �������, ����� ������ �������� ������ �� ������
        // �� �� ������� �����, ��� ��� ��� ����� � ���������, �� �� ����� � ��� ����������.
        private Dictionary<char, Button> _colorButtons;

        // ����������� �����
        public Form1()
        {
            InitializeComponent();
            _generator = new ColorMapGenerator();

            // ������������� ������������� �������� ������ � ��������� ������� System.Drawing.Color
            // ���������� ����� ����� �����, ��� �� �������
            _charToColorMapping = new Dictionary<char, Color>
            {
                { 'R', Color.FromArgb(255, 128, 128) }, // ������� �������
                { 'G', Color.FromArgb(128, 255, 128) }, // ������� �������
                { 'B', Color.FromArgb(128, 128, 255) }, // ������� �����
                { 'Y', Color.FromArgb(255, 255, 128) }  // ������� ������
            };

            // ������������� ������ ������� ��� �������� ������
            _colorNamesMapping = new Dictionary<char, string>
            {
                { 'R', "�������" },
                { 'G', "�������" },
                { 'B', "�����" },
                { 'Y', "������" }
            };

            // ������������� ������� ��� �������� ������ �� ������, ������� � ���������
            _colorButtons = new Dictionary<char, Button>();

            // ����������� ���������� ������� Click �� ���� ������� �� ������ colorButtonsPanel
            // � ��������� _colorButtons. ��������������, ��� Tag ������ � ��������� ����������
            // ��� "R", "G", "B", "Y" ��������������.
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    // ���������, ��� Tag ���������� � �������� ��������� �������� �����
                    if (button.Tag != null && button.Tag.ToString().Length == 1 && _charToColorMapping.ContainsKey(button.Tag.ToString()[0]))
                    {
                        char colorChar = button.Tag.ToString()[0];
                        button.Click += ColorButton_Click; // ����������� ����������
                        _colorButtons.Add(colorChar, button); // ��������� ������ �� ������

                        // ������������� ����� � ����� ������ �� ����� ��������
                        button.BackColor = _charToColorMapping[colorChar];
                        button.Text = _colorNamesMapping[colorChar];
                    }
                    else
                    {
                        // ���� Tag �� ���������� ��� �����������, ����� ������� ��������������
                        // ��� ���������� ��� ������.
                        Console.WriteLine($"��������������: ������ '{button.Name}' �� colorButtonsPanel �� ����� ����������� Tag ��� �����.");
                    }
                }
            }

            StartNewGame(); // �������� ����� ���� ��� �������
        }

        /// <summary>
        /// �������� ���� ��������� ������ (���������� ������� ������� � ��������� �����).
        /// </summary>
        private void StartNewGame()
        {
            // �������������� ��������� ����� ��� �������� ������ ������ �������
            _availableColorsForCurrentRound = new List<char> { 'R', 'G', 'B', 'Y' };

            EnableAllColorButtons(); // ���������� � ������ �������� ��� ������

            // ���������� � ���������� ������ ����� ������ ������/���� (���������� 4 �����, ���� ��������)
            GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
        }

        // CreateColorButtons() ������ �� �����, ��� ��� ������ �� ���������

        /// <summary>
        /// ���������� � ���������� ����� �� ������ �������� ��������� _availableColorsForCurrentRound.
        /// </summary>
        /// <param name="desiredUniqueColors">�������� ���������� ���������� ������ ��� ��������� �����.</param>
        private void GenerateAndDisplayMap(int desiredUniqueColors)
        {
            ClearMapDisplay(); // ������� ������ �����

            int rows = 5; // ������� �����
            int cols = 5;

            try
            {
                // ���������� ���������������� GenerateMap, ������� ��������� ������ ��������� ������
                _currentMap = _generator.GenerateMap(rows, cols, desiredUniqueColors, _availableColorsForCurrentRound);
                DisplayMap(_currentMap); // ���������� ��������������� �����
                DetermineCorrectAnswer(_currentMap); // ���������� ���������� ����� ��� ���� �����
            }
            catch (ArgumentException ex)
            {
                // ��� ������ ���������, �������� ���� ������ � ��������� ���� �����������
                MessageBox.Show(
                    $"������ ��������� �����: {ex.Message}\n���� ��������������.",
                    "������",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame();
            }
            catch (InvalidOperationException ex)
            {
                // ��� ������ ��������, �������� ���� ������ � ��������� ���� �����������
                MessageBox.Show(
                    $"������ �������� ��� ��������� �����: {ex.Message}\n���� ��������������.",
                    "������",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame();
            }
        }

        /// <summary>
        /// ������� ��� ������ � �����.
        /// </summary>
        private void ClearMapDisplay()
        {
            colorMapPanel.Controls.Clear();
            // ����������� ������� ������ �������
            foreach (var control in colorMapPanel.Controls.OfType<Panel>())
            {
                control.Dispose();
            }
        }

        /// <summary>
        /// ���������� ��������������� ����� �� TableLayoutPanel.
        /// </summary>
        /// <param name="map">��������� ������ MapCell.</param>
        private void DisplayMap(MapCell[,] map)
        {
            colorMapPanel.SuspendLayout(); // ���������������� ���������� ��� ��������� ������������������

            // ��������� TableLayoutPanel ��� ����������� ���������� ����� � ��������
            colorMapPanel.RowCount = map.GetLength(0);
            colorMapPanel.ColumnCount = map.GetLength(1);

            // ������� � ����������� ����� �����/��������, ����� ��� ���������� ��������������
            colorMapPanel.RowStyles.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / map.GetLength(0)));
            }

            colorMapPanel.ColumnStyles.Clear();
            for (int c = 0; c < map.GetLength(1); c++)
            {
                colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / map.GetLength(1)));
            }

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    MapCell cell = map[r, c];
                    Panel cellPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(2), // ��������� ������� ����� ��������
                        BackColor = _charToColorMapping[cell.Color],
                        Tag = cell.Color.ToString()
                    };

                    colorMapPanel.Controls.Add(cellPanel, c, r); // ��������� ������ � TableLayoutPanel
                }
            }
            colorMapPanel.ResumeLayout(true); // ������������ ����������
            colorMapPanel.Refresh(); // �������������� ����������, ����� ���������, ��� ��� ������������
        }

        /// <summary>
        /// ���������� ���������� ���� (���, �������� ������ �����) �� ������ ���������� �����.
        /// </summary>
        /// <param name="map">����� MapCell ��� �������.</param>
        private void DetermineCorrectAnswer(MapCell[,] map)
        {
            Dictionary<char, int> counts = _generator.CountColors(map);

            // ��������� ��������, �������� ������ �� �����, ��� ������ ������� (� �����)
            // �� ���������� _availableColorsForCurrentRound, ������� ��������� ���, ����� ����� ������� � ����.
            counts = counts.Where(kv => _availableColorsForCurrentRound.Contains(kv.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);

            if (counts.Any())
            {
                _correctColorChar = counts.OrderByDescending(kv => kv.Value).First().Key;
            }
            else
            {
                _correctColorChar = '\0'; // ��� ����� ������, ��� ������
            }
        }

        /// <summary>
        /// ���������� ������� ����� �� ������ �����.
        /// </summary>
        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            char selectedColor = clickedButton.Tag.ToString()[0];

            if (selectedColor == _correctColorChar)
            {
                // ���������� �����: ������� ���� �� ��������� � ���������� ����� ����
                MessageBox.Show(
                    $"���������! ���� '{_colorNamesMapping[selectedColor]}' ����� �� ����.", // ���������� _colorNamesMapping
                    "�����",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );

                clickedButton.Enabled = false; // ������������ ������
                clickedButton.Visible = false; // �������� ������

                // ������� ��������� ���� �� ������ ��� �������� ������
                _availableColorsForCurrentRound.Remove(selectedColor);

                // ���������, ������� ������ �������� ��� �������� ������
                if (_availableColorsForCurrentRound.Count <= 1) // ���� ������� 1 ��� 0 ������
                {
                    // ��� ����� ������� � ���� ������, ���� ���������� ��������� ������
                    MessageBox.Show(
                        "��� ����� ������� � ���� ������! ���������� ����� ����.",
                        "������� ��������",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.None
                    );
                    StartNewGame(); // �������� ���� ��������� ������
                }
                else
                {
                    // ���������� ������� �����, ��������� ����� ���� � ����������� �������
                    // ���������� ���������� ������ ��� ����� ��������� - ��� ���������� ���������� ������
                    GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
                }
            }
            else
            {
                // ������������ �����: ���� ���������� ��������� ������
                MessageBox.Show(
                    "�����������! ���� ���������� ������.",
                    "������",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame(); // �������� ���� ��������� ������
            }
        }

        /// <summary>
        /// ���������� � ������ �������� ��� ������ ������ �����.
        /// ���� ����� ������ ���������� ��� StartNewGame().
        /// </summary>
        private void EnableAllColorButtons()
        {
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = true;
                    button.Visible = true;
                }
            }
        }

        /// <summary>
        /// ������������ ��� ������ ������ �����. (���� ����� ������ �� ������������, �� �������� ��� �������)
        /// </summary>
        private void DisableAllColorButtons()
        {
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = false;
                }
            }
        }
    }
}