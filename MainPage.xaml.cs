//using Microsoft.Maui.Controls.Compatibility.Platform.UWP;

namespace PerfectPay;

public partial class MainPage : ContentPage
{
	//int count = 0;
	//Var para obtener los valores del Total de la factura bill, propina tip y nroPersonas 
	decimal bill;
	decimal tip;
	int nroPersons = 1;
	public MainPage()
	{
		InitializeComponent();
	}

	//Evento que indica cuando terminaron de ingresar el valor del total de la factura bill
    private void txtBill_Completed(object sender, EventArgs e)
    {
		//Debemos hacer una conversión ya que esto no esta implicito...
		//de String a Decimal
		bill = decimal.Parse(txtBill.Text);
		
		//Creamos y definimos un metodo
		CalculateTotal();
    }

    private void CalculateTotal()
    {
		//Operaciones para determinar los totales a pagar Factura + Propina
		//Total propina TIP
		var totalTip =
			(bill * tip) / 100;

        //Total Por Persona
		var tipByPerson =
			(totalTip ) / nroPersons;
		//Convertirlo a formato de Currency o Moneda
		lblTipByPerson.Text = $"{tipByPerson:C}";

		//SubTotal por persona
		var subtotal = (bill / nroPersons);
        //Convertirlo a formato de Currency o Moneda
        lblSubTotal.Text = $"{subtotal:C}";

		//Total a Pagar por Persona
		var totalByPerson =
			(bill + totalTip) / nroPersons;
        //Convertirlo a formato de Currency o Moneda
        lblTotal.Text = $"{totalByPerson:C}";

    }

    //Evento que indica cuando terminaron de ingresar el valor de la propina tip
    private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		//Conversión del Valor a Decimal...
		tip= (decimal)sldTip.Value;
		//Con una cadena interpolada mostramos el % de la propina.
		lblTip.Text = $"Tip: {tip.ToString("0.##")}%";
		CalculateTotal();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		//Primero determinar, que el objeto que activo este evento sea un elemento Button -> sender
		if (sender is Button)
		{
			//Obtener el elemnto como Boton y no como objeto
			var btn = (Button)sender;
			//Obtenemos solamente el valor quitando el caracter %
			var percentage =
				decimal.Parse(btn.Text.Replace("%", ""));
			//Lo pasamos al control
			sldTip.Value = (double)percentage;
		}
    }

    private void btnMinus_Clicked(object sender, EventArgs e)
    {
		//Primero preguntar si el valor es mayor  1
		//Antes de restarle 1 al valor actual
		if (nroPersons>1)
		{
			nroPersons--;
        }
        lblNroPersons.Text = nroPersons.ToString();
		CalculateTotal();
    }

    private void btnPlus_Clicked(object sender, EventArgs e)
    {
		//Sumarle 1 al valor Actual
		nroPersons++;
        lblNroPersons.Text = nroPersons.ToString();
        CalculateTotal();
    }
}

