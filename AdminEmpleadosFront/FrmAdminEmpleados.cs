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
            FrmInsertEmpleados frm = new FrmInsertEmpleados();
            frm.modoInsert = EnumModoForm.Alta;
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
            Empleado parametroDeBusqueda = new Empleado ();

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
        }
    }