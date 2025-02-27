namespace tpAgencia_Gpo_2
{
    public partial class Form1 : Form
    {



        private Agencia Agencia;
        private Login Login;
        private MenuAgencia MenuAgencia;
        private FormUsuarioSimple usuarioSimple;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Agencia = new Agencia();

            cargaUsuarios();
            cargaCiudades();


            Login = new Login(Agencia);
            Login.MdiParent = this;
            Login.TransfEventoLogin += TransfDelegadoLogin;
            Login.Show();

            

            //Vuelos hardcodeados

            Agencia.agregarVuelo(Agencia.GetCiudades()[0], Agencia.GetCiudades()[1], 20, 50000, new DateTime(2023, 10, 30), "AA", "Airbus");
            Agencia.agregarVuelo(Agencia.GetCiudades()[1], Agencia.GetCiudades()[2], 50, 100000, new DateTime(2023, 10, 20), "AA", "Airbus320");
            Agencia.agregarVuelo(Agencia.GetCiudades()[2], Agencia.GetCiudades()[0], 2, 200000, new DateTime(2023, 10, 10), "JetSmart", "Airbus300");
            Agencia.agregarVuelo(Agencia.GetCiudades()[0], Agencia.GetCiudades()[1], 2, 200000, new DateTime(2023, 09, 10), "JetSmart", "Airbus300");


            cargaDeHotelesConReservaYusuarios();
        }



        private  void cargaUsuarios()
        {
            Agencia.setUsuario(new Usuario(1, "pepe", "popo", "29111222", "Juan@pepe.com", "1234", true));
            Agencia.setUsuario(new Usuario(2, "juana", "lola", "33333333", "Juana@lola.com", "1234", true));
            Agencia.setUsuario(new Usuario(3, "pablo", "lopez", "22222222", "Pablo@lopez.com", "1234", true));
            Agencia.setUsuario(new Usuario(4, "pablo", "lopez", "22222222", "admin@admin.com", "1234", true));

        }

        private void cargaCiudades()
        {
            Agencia.setCiudad(new Ciudad(1, "Bariloche"));
            Agencia.setCiudad(new Ciudad(2, "Mendoza"));
            Agencia.setCiudad(new Ciudad(3, "Buenos Aires"));
        }

        private void cargaDeHotelesConReservaYusuarios()
        {

            Ciudad? ciudadParaHotelBariloche = Agencia.GetCiudades().ToList().Where(x => x.id == 1).FirstOrDefault();
            Ciudad? ciudadParaHotel2 = Agencia.GetCiudades().ToList().Where(x => x.id == 2).FirstOrDefault();
            Ciudad? ciudadParaHotel3 = Agencia.GetCiudades().ToList().Where(x => x.id == 3).FirstOrDefault();

            List<Usuario> ListHuespedesFaena = new List<Usuario>();
            Usuario usuarioFaena = Agencia.getUsuarios().ToList().Where(x => x.id == 1).FirstOrDefault();
            ListHuespedesFaena.Add(usuarioFaena);
            Usuario usuarioFaena2 = Agencia.getUsuarios().ToList().Where(x => x.id == 2).FirstOrDefault();
            ListHuespedesFaena.Add(usuarioFaena2);

            List<ReservaHotel> reservas = new List<ReservaHotel>();
            Hotel hotelFaena = new Hotel(1, ciudadParaHotelBariloche, 2, 1000, "Faena", ListHuespedesFaena, reservas);

            Hotel hotelHilton = new Hotel(2, ciudadParaHotelBariloche, 2, 1000, "Hilton", ListHuespedesFaena, reservas);
            ReservaHotel reservaHotel = new ReservaHotel(hotelFaena, usuarioFaena2, new DateTime(2023, 03, 01), new DateTime(2023, 03, 14), 1000);
            ReservaHotel reservaHotel2 = new ReservaHotel(hotelFaena, usuarioFaena2, new DateTime(2023, 03, 15), new DateTime(2023, 03, 20), 1000);
            reservas.Add(reservaHotel);
            reservas.Add(reservaHotel2);

            List<Usuario> ListHuespedesHilton = new List<Usuario>();
            Usuario usuarioHilton = Agencia.getUsuarios().ToList().Where(x => x.id == 1).FirstOrDefault();
            ListHuespedesHilton.Add(usuarioHilton);

            Agencia.setHotel(hotelFaena);
            Agencia.setHotel(hotelHilton);
        }

        private void TransfDelegadoLogin()
        {
            MessageBox.Show("Log correcto, Usuario: " + Agencia.nombreLogueado(), "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login.Close();
            MenuAgencia = new MenuAgencia(Agencia, this);
            MenuAgencia.MdiParent = this;
            MenuAgencia.Show();
        }


    }
}