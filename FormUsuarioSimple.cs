﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static tpAgencia_Gpo_2.FormUsuario;

namespace tpAgencia_Gpo_2
{
    public partial class FormUsuarioSimple : Form
    {
        private Agencia refAgencia;
        private Form1 form1;
        private Usuario usuarioActual;
        public TransfDelegadoFormUsuarioSimple TransfEventoFormUsuarioSimple;
        public FormUsuarioSimple(Agencia agencia)
        {
            InitializeComponent();
            this.refAgencia = agencia;
            this.form1 = form1;
            this.MdiParent = form1;
            usuarioActual = agencia.getUsuarioActual();
            this.WindowState = FormWindowState.Maximized;

            //seleccionado = new(1, "juan", "Perez", "33444555", "juan@perez.com", "12345", false);
            //agencia.agregarUsuarioobjet(seleccionado);
            //agencia.setUsuarioActual(seleccionado);
            actualizarDatos();

        }
        public delegate void TransfDelegadoFormUsuarioSimple();
        private void FormUsuarioSimple_Load(object sender, EventArgs e)
        {

        }

        private void button_agregar_credito_Click(object sender, EventArgs e)
        {

            if (usuarioActual != null)
            {
                if (refAgencia.agregarCredito(usuarioActual.id, double.Parse(textBox_MiCredito.Text)))
                {

                    MessageBox.Show("Modificado con éxito");
                }

                else
                {
                    MessageBox.Show("Problemas al modificar");//corregir problema de porque entra aca
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }
            actualizarDatos();
        }



        private void button_modificar_password_Click(object sender, EventArgs e)
        {

            if (usuarioActual != null)
            {
                if (refAgencia.getUsuarioActual().password == textBox_pass_viejo.Text)
                {

                    if (refAgencia.modificarPassword(usuarioActual.id, textBox_pass_nuevo.Text))
                    {

                        MessageBox.Show("Modificado con éxito");
                    }

                    else
                    {
                        MessageBox.Show("Problemas al modificar");//corregir problema de porque entra aca
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }
            actualizarDatos();
        }

        private void actualizarDatos()
        {
            textBox_MiCredito.Text = string.Empty;

            if (usuarioActual != null)
            {
                label_ver_saldo_credito.Text = refAgencia.getUsuarioActual().credito.ToString();
                usuarioActual = refAgencia.getUsuarioActual();
                label_set_usuario_actual.Text = refAgencia.getUsuarioActual().name;
                label_ver_saldo_credito.Text = (refAgencia.getUsuarioActual()?.credito ?? 0) == 0 ? "No posee saldo actual" : refAgencia.getUsuarioActual().credito.ToString();
                textBox_id.Text = refAgencia.getUsuarioActual().id.ToString();
                textBox_nombre.Text = refAgencia.getUsuarioActual().name; ;
                textBox_apellido.Text = refAgencia.getUsuarioActual().apellido; ;
                textBox_email.Text = refAgencia.getUsuarioActual().mail;
                textBox_dni.Text = refAgencia.getUsuarioActual().dni;
            }


        }

        private void button_Modificar_Click(object sender, EventArgs e)
        {
            if (usuarioActual != null)
            {
                if (refAgencia.modificarUsuario(usuarioActual.id, textBox_nombre.Text, textBox_apellido.Text, textBox_dni.Text, textBox_email.Text))
                {
                    MessageBox.Show("Modificado con éxito");
                }
                else
                {
                    MessageBox.Show("Problemas al modificar");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }

            actualizarDatos();
        }

        private void Volver_desde_usuario_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuAgencia MenuAgencia = new MenuAgencia(refAgencia, form1);
            MenuAgencia.MdiParent = form1;
            MenuAgencia.Show();
        }
    }
}
