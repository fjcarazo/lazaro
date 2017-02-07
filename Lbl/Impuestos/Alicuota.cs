using System;
using System.Collections.Generic;
using System.Text;
using Lazaro.Orm;
using Lazaro.Orm.Attributes;

namespace Lbl.Impuestos
{
        /// <summary>
        /// Representa una alícuota de IVA.
        /// </summary>
        [Lbl.Atributos.Nomenclatura(NombreSingular = "Alícuota", Grupo = "Impuestos")]
        [Lbl.Atributos.Datos(TablaDatos = "alicuotas", CampoId = "id_alicuota")]
        [Lbl.Atributos.Presentacion()]

        [Entity(TableName = "alicuotas", IdFieldName = "id_alicuota")]
        public class Alicuota : ElementoDeDatos, IEntity<Alicuota>
        {
                public Alicuota(Lfx.Data.IConnection dataBase)
                        : base(dataBase) { }

                public Alicuota(Lfx.Data.IConnection dataBase, int itemId)
			: base(dataBase, itemId) { }

                public Alicuota(Lfx.Data.IConnection dataBase, Lfx.Data.Row row)
                        : base(dataBase, row) { }


                [Column(Name = "porcentaje", Type = ColumnTypes.Numeric)]
                public decimal Porcentaje
                {
                        get
                        {
                                return this.GetFieldValue<decimal>("porcentaje");
                        }
                        set
                        {
                                this.Registro["porcentaje"] = value;
                        }
                }

                [Column(Name = "importe_minimo", Type = ColumnTypes.Numeric)]
                public decimal ImporteMinimo
                {
                        get
                        {
                                return this.GetFieldValue<decimal>("importe_minimo");
                        }
                        set
                        {
                                this.Registro["importe_minimo"] = value;
                        }
                }


                public override Lfx.Types.OperationResult Guardar()
                {
                        qGen.IStatement Comando;

                        if (this.Existe == false) {
                                Comando = new qGen.Insert(this.TablaDatos);
                        } else {
                                Comando = new qGen.Update(this.TablaDatos);
                                Comando.WhereClause = new qGen.Where(this.CampoId, this.Id);
                        }

                        Comando.ColumnValues.AddWithValue("nombre", this.Nombre);
                        Comando.ColumnValues.AddWithValue("porcentaje", this.Porcentaje);
                        Comando.ColumnValues.AddWithValue("importe_minimo", this.ImporteMinimo);

                        this.AgregarTags(Comando);

                        this.Connection.Execute(Comando);

                        return base.Guardar();
                }
        }
}
