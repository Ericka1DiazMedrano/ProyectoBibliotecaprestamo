using System.Data;
using VeterinariaSystem.Models;
using VeterinariaSystem.Services;

namespace VeterinariaSystem;

public class FormMain : Form
{
    private readonly VeterinariaService _service;

    private TabControl tabControlMain = new();
    private TabPage tabClientes = new("Clientes");
    private TabPage tabMascotas = new("Mascotas");
    private TabPage tabConsultas = new("Consultas Clínicas");

    private DataGridView dgvClientes = new();
    private DataGridView dgvMascotas = new();
    private DataGridView dgvConsultas = new();

    private TextBox txtClienteNombre = new(), txtClienteTel = new(), txtClienteEmail = new();
    private Button btnGuardarCliente = new(), btnEliminarCliente = new();
    private int clienteSeleccionadoId = 0;

    private TextBox txtMascotaNombre = new(), txtMascotaEspecie = new();
    private ComboBox cmbPropietarios = new();
    private Button btnGuardarMascota = new();

    private ComboBox cmbConsultaMascotas = new();
    private TextBox txtMotivo = new(), txtDiagnostico = new();
    private Button btnRegistrarConsulta = new();

    public FormMain()
    {
        _service = new VeterinariaService();
        InitializeComponentCustom();
        CargarDatos();
    }

    private void InitializeComponentCustom()
    {
        this.Text = "Sistema de Control Clínico Veterinario v2";
        this.Size = new Size(950, 650);
        this.StartPosition = FormStartPosition.CenterScreen;

        tabControlMain.Dock = DockStyle.Fill;
        this.Controls.Add(tabControlMain);

        tabControlMain.TabPages.Add(tabClientes);
        tabControlMain.TabPages.Add(tabMascotas);
        tabControlMain.TabPages.Add(tabConsultas);

        BuildClientesTab();
        BuildMascotasTab();
        BuildConsultasTab();
    }

    private void BuildClientesTab()
    {
        Panel panelForm = new() { Dock = DockStyle.Top, Height = 120 };

        panelForm.Controls.Add(new Label { Text = "Nombre:", Location = new Point(20, 20), AutoSize = true });
        txtClienteNombre.Location = new Point(90, 18); txtClienteNombre.Width = 200;
        panelForm.Controls.Add(txtClienteNombre);

        panelForm.Controls.Add(new Label { Text = "Teléfono:", Location = new Point(310, 20), AutoSize = true });
        txtClienteTel.Location = new Point(380, 18); txtClienteTel.Width = 150;
        panelForm.Controls.Add(txtClienteTel);

        panelForm.Controls.Add(new Label { Text = "Email:", Location = new Point(550, 20), AutoSize = true });
        txtClienteEmail.Location = new Point(600, 18); txtClienteEmail.Width = 200;
        panelForm.Controls.Add(txtClienteEmail);

        btnGuardarCliente.Text = "Guardar Cliente";
        btnGuardarCliente.Location = new Point(90, 60);
        btnGuardarCliente.Click += BtnGuardarCliente_Click;
        panelForm.Controls.Add(btnGuardarCliente);

        btnEliminarCliente.Text = "Eliminar Seleccionado";
        btnEliminarCliente.Location = new Point(220, 60);
        btnEliminarCliente.Click += BtnEliminarCliente_Click;
        panelForm.Controls.Add(btnEliminarCliente);

        dgvClientes.Dock = DockStyle.Fill;
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClientes.CellClick += DgvClientes_CellClick;

        tabClientes.Controls.Add(dgvClientes);
        tabClientes.Controls.Add(panelForm);
    }

    private void BtnGuardarCliente_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtClienteNombre.Text)) return;

        var cliente = new Cliente
        {
            Id = clienteSeleccionadoId,
            Nombre = txtClienteNombre.Text,
            Telefono = txtClienteTel.Text,
            Email = txtClienteEmail.Text
        };

        _service.GuardarCliente(cliente);
        LimpiarFormCliente();
        CargarDatos();
    }

    private void BtnEliminarCliente_Click(object? sender, EventArgs e)
    {
        if (clienteSeleccionadoId > 0)
        {
            _service.EliminarCliente(clienteSeleccionadoId);
            LimpiarFormCliente();
            CargarDatos();
        }
    }

    private void DgvClientes_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && dgvClientes.Rows[e.RowIndex].DataBoundItem is Cliente cliente)
        {
            clienteSeleccionadoId = cliente.Id;
            txtClienteNombre.Text = cliente.Nombre;
            txtClienteTel.Text = cliente.Telefono;
            txtClienteEmail.Text = cliente.Email;
        }
    }

    private void LimpiarFormCliente()
    {
        clienteSeleccionadoId = 0;
        txtClienteNombre.Clear();
        txtClienteTel.Clear();
        txtClienteEmail.Clear();
    }

    private void BuildMascotasTab()
    {
        Panel panelForm = new() { Dock = DockStyle.Top, Height = 120 };

        panelForm.Controls.Add(new Label { Text = "Nombre:", Location = new Point(20, 20), AutoSize = true });
        txtMascotaNombre.Location = new Point(90, 18); txtMascotaNombre.Width = 150;
        panelForm.Controls.Add(txtMascotaNombre);

        panelForm.Controls.Add(new Label { Text = "Especie:", Location = new Point(260, 20), AutoSize = true });
        txtMascotaEspecie.Location = new Point(320, 18); txtMascotaEspecie.Width = 120;
        panelForm.Controls.Add(txtMascotaEspecie);

        panelForm.Controls.Add(new Label { Text = "Propietario:", Location = new Point(460, 20), AutoSize = true });
        cmbPropietarios.Location = new Point(540, 18); cmbPropietarios.Width = 200;
        panelForm.Controls.Add(cmbPropietarios);

        btnGuardarMascota.Text = "Registrar Mascota";
        btnGuardarMascota.Location = new Point(90, 60);
        btnGuardarMascota.Click += BtnGuardarMascota_Click;
        panelForm.Controls.Add(btnGuardarMascota);

        dgvMascotas.Dock = DockStyle.Fill;
        tabMascotas.Controls.Add(dgvMascotas);
        tabMascotas.Controls.Add(panelForm);
    }

    private void BtnGuardarMascota_Click(object? sender, EventArgs e)
    {
        if (cmbPropietarios.SelectedValue is not int clienteId || string.IsNullOrWhiteSpace(txtMascotaNombre.Text)) return;

        var mascota = new Mascota
        {
            Nombre = txtMascotaNombre.Text,
            Especie = txtMascotaEspecie.Text,
            ClienteId = clienteId
        };

        _service.GuardarMascota(mascota);
        txtMascotaNombre.Clear();
        txtMascotaEspecie.Clear();
        CargarDatos();
    }

    private void BuildConsultasTab()
    {
        Panel panelForm = new() { Dock = DockStyle.Top, Height = 150 };

        panelForm.Controls.Add(new Label { Text = "Mascota:", Location = new Point(20, 20), AutoSize = true });
        cmbConsultaMascotas.Location = new Point(90, 18); cmbConsultaMascotas.Width = 200;
        panelForm.Controls.Add(cmbConsultaMascotas);

        panelForm.Controls.Add(new Label { Text = "Motivo:", Location = new Point(310, 20), AutoSize = true });
        txtMotivo.Location = new Point(380, 18); txtMotivo.Width = 250;
        panelForm.Controls.Add(txtMotivo);

        panelForm.Controls.Add(new Label { Text = "Diagnóstico:", Location = new Point(20, 60), AutoSize = true });
        txtDiagnostico.Location = new Point(90, 58); txtDiagnostico.Width = 200;
        panelForm.Controls.Add(txtDiagnostico);

        btnRegistrarConsulta.Text = "Registrar Consulta";
        btnRegistrarConsulta.Location = new Point(90, 100);
        btnRegistrarConsulta.Click += BtnRegistrarConsulta_Click;
        panelForm.Controls.Add(btnRegistrarConsulta);

        dgvConsultas.Dock = DockStyle.Fill;
        tabConsultas.Controls.Add(dgvConsultas);
        tabConsultas.Controls.Add(panelForm);
    }

    private void BtnRegistrarConsulta_Click(object? sender, EventArgs e)
    {
        if (cmbConsultaMascotas.SelectedValue is not int mascotaId) return;

        var consulta = new Consulta
        {
            MascotaId = mascotaId,
            VeterinarioId = 1,
            Motivo = txtMotivo.Text,
            Diagnostico = txtDiagnostico.Text,
            Fecha = DateTime.Now,
            Costo = 50.00m
        };

        _service.RegistrarConsulta(consulta);
        txtMotivo.Clear();
        txtDiagnostico.Clear();
        CargarDatos();
    }

    private void CargarDatos()
    {
        var clientes = _service.ObtenerClientes();
        dgvClientes.DataSource = null;
        dgvClientes.DataSource = clientes;

        cmbPropietarios.DataSource = clientes;
        cmbPropietarios.DisplayMember = "Nombre";
        cmbPropietarios.ValueMember = "Id";

        var mascotas = _service.ObtenerMascotasConPropietario();
        dgvMascotas.DataSource = null;
        dgvMascotas.DataSource = mascotas.Select(m => new {
            m.Id,
            m.Nombre,
            m.Especie,
            Propietario = m.Cliente != null ? m.Cliente.Nombre : "N/A"
        }).ToList();

        cmbConsultaMascotas.DataSource = mascotas;
        cmbConsultaMascotas.DisplayMember = "Nombre";
        cmbConsultaMascotas.ValueMember = "Id";

        var consultas = _service.ObtenerHistorialClinico();
        dgvConsultas.DataSource = null;
        dgvConsultas.DataSource = consultas.Select(c => new {
            c.Id,
            Fecha = c.Fecha.ToString("dd/MM/yyyy HH:mm"),
            Mascota = c.Mascota.Nombre,
            c.Motivo,
            c.Diagnostico
        }).ToList();
    }
}
