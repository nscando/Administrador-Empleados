using AdminEmpleadosEntidades;
using AdminEmpleadosNegocio;

namespace AdminEmpleadosFront
    {
    public partial class FrmAdminEmpleados : Form
        {
        private List<Empleado> empleadosList = new List<Empleado>();

        public FrmAdminEmpleados ()
            {
            InitializeComponent();
            }

        private void btnSalir_Click ( object sender, EventArgs e )
            {
            }

        private void btnEliminar_Click ( object sender, EventArgs e )
            {
            if ( empleadoBindingSource.Current == null )
                return;

            Empleado emp = (Empleado) empleadoBindingSource.Current;

            DialogResult res = MessageBox.Show("¿Confirma que desea anular el empleado " + emp.Nombre + " ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if ( res == DialogResult.No )
                {
                return;
                }
            emp.anulado = true;

            try
                {
                EmpleadosNegocio.Update(emp);
                MessageBox.Show("El empleado " + emp.Nombre + " fue anulado correctamente", "Anulación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            catch ( Exception ex ) { MessageBox.Show(ex.Message); }
            buscarEmpleados();

            }

        private void btnModificar_Click ( object sender, EventArgs e )
            {
            if ( empleadoBindingSource.Current == null )
                {

                return;
                }
            FrmEditEmpleados frm = new FrmEditEmpleados();
            frm.modo = EnumModoForm.Modificacion;
            frm._empleado = (Empleado) empleadoBindingSource.Current;

            frm.ShowDialog();
            buscarEmpleados();
            }

        private void btnConsultar_Click ( object sender, EventArgs e )
            {
            if ( empleadoBindingSource.Current == null )
                {
                return;
                }
            FrmEditEmpleados frm = new FrmEditEmpleados();

            frm.modo = EnumModoForm.Consulta;
            frm._empleado = (Empleado) empleadoBindingSource.Current;
            frm.ShowDialog();
            buscarEmpleados();
            }

        private void btnAlta_Click ( object sender, EventArgs e )
            {
            FrmEditEmpleados frm = new FrmEditEmpleados();
            frm.modo = EnumModoForm.Alta;
            frm.ShowDialog();

            buscarEmpleados();
            }

        private void btnBuscar_Click ( object sender, EventArgs e )
            {
            buscarEmpleados();
            }

        private void buscarEmpleados ()
            {
            //Obtengo el nombre ingresado por el usuario
            string nombreBuscar = txtBusqueda.Text.Trim().ToUpper();

            //declaro el parametro de busqueda
            Empleado parametroDeBusqueda = new Empleado();

            if ( !String.IsNullOrEmpty(nombreBuscar.Trim()) )
                parametroDeBusqueda.Nombre = nombreBuscar;

            //busco lista de empleados en la capa de negocio, pasandole el parametro de busqueda
            empleadosList = EmpleadosNegocio.Get(parametroDeBusqueda);

            //actualizo grilla
            refreshGrid();
            }

        private void refreshGrid ()
            {
            //Actualizo el Binding con la lista de empleados que viene desde la DB
            empleadoBindingSource.DataSource = null;
            empleadoBindingSource.DataSource = empleadosList;
            }

        private void txtBusqueda_KeyPress ( object sender, KeyPressEventArgs e )
            {
            if ( e.KeyChar == (char) Keys.Enter )
                {
                buscarEmpleados();
                }
            }

        private void FrmAdminEmpleados_Load ( object sender, EventArgs e )
            {

            }
        }
    }