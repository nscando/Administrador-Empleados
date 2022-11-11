using AdminEmpleadosEntidades;
using AdminEmpleadosNegocio;
using System.ComponentModel;

namespace AdminEmpleadosFront
    {
    public partial class FrmEditEmpleados : Form
        {
        //enumaercion la utilizamos para asignar valores a numeros
        public EnumModoForm modo = EnumModoForm.Alta;
        public Empleado _empleado = new Empleado();
        public FrmEditEmpleados ()
            {
            InitializeComponent();
            }

        private void btnAceptar_Click ( object sender, EventArgs e )
            {
            Guardar();
            }

        private bool ValidarEmpleado ( ref string mensaje, Empleado e )
            {
            mensaje = "";
            //validamos que el DNI no venga vacio
            if ( String.IsNullOrEmpty(e.Dni.Trim()) )
                {
                mensaje += "\nError en DNI";
                }
            //validamos que el NOMBRE no venga vacio
            if ( String.IsNullOrEmpty(e.Nombre.Trim()) )
                {
                mensaje += "\nError en Nombre";
                }

            if ( !String.IsNullOrEmpty(mensaje) )
                {
                return false;
                }
            //SI ESTA TODO OK RETORNA EL TRUE
            return true;
            }

        private void LimpiarControles ()
            {
            txtId.Text = "";
            txtSalario.Value = 0;
            txtDireccion.Text = "";
            txtDni.Text = "";
            txtIngreso.Value = DateTime.Now;
            txtNombre.Text = "";
            }

        private void Guardar ()
            {
            try
                {
                //tomamos todos los valores del formulario y los guardamos en un objeto
                //de tipo empleado
                Empleado emp = new Empleado();

                //completamos con los valores cargados en el formulario
                emp.Salario = txtSalario.Value;
                //con trim eliminamos los espacios
                emp.Direccion = txtDireccion.Text.Trim();
                emp.Dni = txtDni.Text.Trim();
                emp.FechaIngreso = txtIngreso.Value;
                emp.Departamento = new Departamento();
                emp.Departamento.id = (int) cbmDepartamento.SelectedValue;
                emp.Nombre = txtNombre.Text.Trim();

                string mensajeErrores = "";

                //hacemos las validaciones con el mensaje por referencia
                if ( !ValidarEmpleado(ref mensajeErrores, emp) )
                    {
                    //si fallan las validaciones mostramos el mensaje con el error
                    MessageBox.Show("Atencion: Se en contraron los siguientes errores \n" +
                        mensajeErrores, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    }
                //si las validacion estan ok
                //se pregunta si quiere guardar los datos
                DialogResult res = MessageBox.Show("¿Confirma guardar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //si ponemos que no salimos del cuadro de mensaje y no se hace nada
                if ( res == DialogResult.No )
                    //Dialog result es una enumeracion
                    {
                    return;
                    }

                //guardamos los datos
                if ( modo == EnumModoForm.Alta )
                    {

                    //lo que devuelve el INSERT lo guardo en la variable "idEmp"
                    int idEmp = EmpleadosNegocio.Insert(emp);
                    //convierto los datos de "idEmp" a String para mostrarlos en el TextBox del Front
                    txtId.Text = idEmp.ToString();

                    //mostramos el msg con el ID del empleado generado
                    MessageBox.Show("Se genero el empleado nro " + idEmp.ToString(), "Empleado creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                if ( modo == EnumModoForm.Modificacion )
                    {
                    emp.id = Convert.ToInt32(txtId.Text);
                    EmpleadosNegocio.Update(emp);

                    MessageBox.Show("Los datos se actualizaron correctamente", "Empleado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                LimpiarControles();

                }
            catch ( Exception ex ) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        private void FrmInsertEmpleados_Load ( object sender, EventArgs e )
            {
            CargarComboDepartamento();
            
            //en este caso la enumeracion refleja si el formulario se abre en Modo ALTA-MODIF-CONSULTA
            if ( modo == EnumModoForm.Alta )
                {
                //limpiamos los controles, dejamos todo vacio
                LimpiarControles();
                HabilitarControles(true);
                }
            if ( modo == EnumModoForm.Modificacion )
                {
                HabilitarControles(true);
                CargarDatos();

                }
            if ( modo == EnumModoForm.Consulta )
                {
                HabilitarControles(false);
                CargarDatos();
                btnAceptar.Enabled = false;
                }
            }

        private void CargarComboDepartamento ()
            {
            Departamento d = new Departamento();
            departamentoBindingSource.DataSource = DepartamentosNegocio.Get(d);
            }

        private void btnCancelar_Click ( object sender, EventArgs e )
            {
            //cierra ventana
            Close();
            }

        private void HabilitarControles ( bool habilitar )
            {
            txtSalario.Enabled = habilitar;
            txtDni.Enabled = habilitar;
            txtDireccion.Enabled = habilitar;
            txtIngreso.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            cbmDepartamento.Enabled = habilitar;
            }

        private void CargarDatos ()
            {
            txtId.Text = _empleado.id.ToString();
            txtSalario.Value = Convert.ToDecimal(_empleado.Salario);
            txtDireccion.Text = _empleado.Direccion;
            txtDni.Text = _empleado.Dni;
            if ( _empleado.FechaIngreso != null )
                {
                txtIngreso.Value = Convert.ToDateTime(_empleado.FechaIngreso);
                txtNombre.Text = _empleado.Nombre;
                }

            if ( _empleado.Departamento != null )
                {
                cbmDepartamento.SelectedValue = _empleado.Departamento.id;
                }
            }

        private void bindingSource1_CurrentChanged ( object sender, EventArgs e )
            {

            }

        private void txt_Validating ( object sender, CancelEventArgs e )
            {
            errorProvider1.Clear();
            if ( String.IsNullOrEmpty(txtDni.Text.Trim()) )
                {
                errorProvider1.SetError(txtDni, "Ingrese el DNI");
                }
            if ( String.IsNullOrEmpty(txtNombre.Text.Trim()) )
                {
                errorProvider1.SetError(txtNombre, "Ingrese el nombre");
                }
            }

        }


    }
