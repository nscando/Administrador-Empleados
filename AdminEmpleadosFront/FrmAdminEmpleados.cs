using AdminEmpleadosEntidades;
using AdminEmpleadosNegocio;

namespace AdminEmpleadosFront
    {
    public partial class FrmAdminEmpleados : Form
        {

        List<Empleado> empleadosList= new List<Empleado>();

        public FrmAdminEmpleados ()
            {
            InitializeComponent();
            }

        private void btnSalir_Click ( object sender, EventArgs e )
            {

            }

        private void btnEliminar_Click ( object sender, EventArgs e )
            {

            }

        private void btnModificar_Click ( object sender, EventArgs e )
            {

            }

        private void btnConsultar_Click ( object sender, EventArgs e )
            {

            }

        private void btnAlta_Click ( object sender, EventArgs e )
            {

            }

        private void btnBuscar_Click ( object sender, EventArgs e )
            {
            buscarEmpleados();
            }

        private void buscarEmpleados ()
            {
            Empleado parametroDeBusqueda = new Empleado ();
            empleadosList = EmpleadosNegocio.Get(parametroDeBusqueda);
            refreshGrid();
            }

        private void refreshGrid ()
            {
            empleadoBindingSource.DataSource = null;
            empleadoBindingSource.DataSource = empleadosList;

            }


        }
    }