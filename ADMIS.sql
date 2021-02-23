USE [ADMIS_AI]
GO
/****** Object:  Table [dbo].[Tb_Abonos]    Script Date: 24/05/2017 20:59:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Abonos](
	[Codigo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Credito] [int] NOT NULL,
	[Valor] [float] NOT NULL,
	[Estado] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_AbonosCuotas]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_AbonosCuotas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cuotas] [int] NOT NULL,
	[FechaAbono] [date] NOT NULL,
	[ValorAbono] [float] NOT NULL,
	[Recargo] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Clientes]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Clientes](
	[Identificacion] [int] NOT NULL,
	[Nombre1] [varchar](30) NOT NULL,
	[Nombre2] [varchar](30) NULL,
	[Apellido1] [varchar](20) NOT NULL,
	[Apellido2] [varchar](20) NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Celular] [varchar](20) NOT NULL,
	[Email] [varchar](50) NULL,
	[Estado] [varchar](15) NOT NULL,
	[TipoCliente] [varchar](15) NOT NULL,
	[Direccion] [varchar](45) NOT NULL,
	[Ciudad] [varchar](20) NOT NULL,
	[Cupo_activo] [char](1) NOT NULL,
	[Solicitud] [varchar](20) NULL,
	[Tipo_Documento_Codigo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Configuraciones]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Configuraciones](
	[Codigo] [int] NOT NULL,
	[AumentoFinanciacion] [float] NULL,
	[Tiempo] [int] NULL,
	[IVA] [float] NULL,
	[Recargo] [float] NULL,
	[MontoMinorista] [float] NOT NULL,
	[MontoMayorista] [float] NOT NULL,
	[CupoCliente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Creditos]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Creditos](
	[Codigo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Estado] [int] NOT NULL,
	[Numero_Pagare] [int] NOT NULL,
	[Cuota_inicial] [float] NULL,
	[Total_Adeudado] [float] NOT NULL,
	[Venta] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Cuotas]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Cuotas](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Financiacion] [int] NOT NULL,
	[NumeroCuotas] [int] NOT NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaLimite] [date] NOT NULL,
	[ValorCuotas] [float] NOT NULL,
	[CuotasAPagar] [int] NOT NULL,
	[FechaAPagar] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Detalle_Entrada]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Detalle_Entrada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [float] NOT NULL,
	[Entrada] [int] NOT NULL,
	[Sucursal] [varchar](6) NOT NULL,
	[ProductoSucursal] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Detalle_Producto_Sucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Detalle_Producto_Sucursal](
	[codigo_detalle] [varchar](20) NOT NULL,
	[Stock_Minimo] [int] NOT NULL,
	[Stock_Maximo] [int] NOT NULL,
	[Valor_Venta] [float] NOT NULL,
	[Valor_Mayor] [float] NOT NULL,
	[Valor_Especial] [float] NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal] ADD [Producto] [varchar](10) NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal] ADD [Sucursal] [varchar](6) NOT NULL
 CONSTRAINT [PK_Tb_Detalle_Producto_Sucursal] PRIMARY KEY CLUSTERED 
(
	[codigo_detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_DetalleTranslados]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_DetalleTranslados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Total] [float] NOT NULL,
	[Fecha] [varchar](45) NOT NULL,
	[Aumento] [float] NOT NULL,
	[Sucursal_Origen] [varchar](6) NOT NULL,
	[ProductoSucursal] [varchar](20) NOT NULL,
	[Sucursal_Destino] [varchar](6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_DetalleVenta]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_DetalleVenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Descuento] [float] NULL,
	[Sub_total] [float] NOT NULL,
	[Venta] [int] NOT NULL,
	[ProductoSucursal] [varchar](20) NOT NULL,
	[Precio] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Dian]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Dian](
	[Codigo] [int] NOT NULL,
	[Resolucion] [varchar](30) NOT NULL,
	[Fecha_inicio_vigencia] [date] NOT NULL,
	[Fecha_fin_vigencia] [date] NOT NULL,
	[Num_inicio_rango] [int] NOT NULL,
	[Num_fin_rango] [int] NOT NULL,
	[Num_actual] [int] NOT NULL,
	[Sucursal] [varchar](6) NOT NULL,
	[Estado] [varchar](12) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Entradas]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Entradas](
	[Codigo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Total] [float] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
	[Sucursal] [varchar](6) NOT NULL,
	[Proveedor] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Estados]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Estados](
	[Id] [int] NOT NULL,
	[Estado] [varchar](45) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Financiacion]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Financiacion](
	[Codigo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Estado] [int] NOT NULL,
	[Numero_Pagare] [int] NOT NULL,
	[Tiempo] [varchar](20) NOT NULL,
	[Aumento] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[Cuota_Inicial] [int] NULL,
	[Total_Adeudado] [float] NOT NULL,
	[Venta] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Permiso]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Permiso](
	[Permiso_id] [int] NOT NULL,
	[Modulo] [varchar](50) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Permiso_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Permiso_Denegado_Roles]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Permiso_Denegado_Roles](
	[RolID] [int] NOT NULL,
	[PermisoID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Post]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Post](
	[Codigo] [int] NOT NULL,
	[Resolucion] [varchar](30) NOT NULL,
	[Sucursal] [varchar](6) NOT NULL,
	[Num_actual] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Productos]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Productos](
	[Codigo_producto] [varchar](10) NOT NULL,
	[Referencia] [varchar](5) NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Tb_Productos] ADD [Descripcion] [varchar](100) NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[Tb_Productos] ADD [Estado] [varchar](15) NOT NULL
ALTER TABLE [dbo].[Tb_Productos] ADD [Tipo_Producto] [varchar](10) NOT NULL
ALTER TABLE [dbo].[Tb_Productos] ADD [cantidad] [int] NULL
PRIMARY KEY CLUSTERED 
(
	[Codigo_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Proveedores]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Proveedores](
	[Identificacion] [int] NOT NULL,
	[Celular] [varchar](20) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Tipo_Documento] [int] NOT NULL,
	[Direccion] [varchar](45) NOT NULL,
	[Nit] [int] NOT NULL,
	[Nombre1] [varchar](30) NOT NULL,
	[Nombre2] [varchar](30) NULL,
	[Apellido1] [varchar](20) NOT NULL,
	[Apellido2] [varchar](20) NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Estado] [varchar](10) NOT NULL,
	[Email] [varchar](45) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Recuperar_contraseña]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Recuperar_contraseña](
	[Codigo] [int] NOT NULL,
	[Respuesta] [varchar](45) NOT NULL,
	[Usuario] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Rol_Usuarios]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Rol_Usuarios](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](45) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Sucursales]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Sucursales](
	[Codigo] [varchar](6) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Apodo] [varchar](15) NOT NULL,
	[Prefijo] [varchar](4) NOT NULL,
	[Telefono] [varchar](45) NOT NULL,
	[Direccion] [varchar](45) NOT NULL,
	[NIT] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Tipo_Documento]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Tipo_Documento](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Tipo_Producto]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Tb_Tipo_Producto](
	[Codigo] [varchar](10) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Usuarios]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Usuarios](
	[Identificacion] [bigint] NOT NULL,
	[Tipo_Documento] [int] NOT NULL,
	[Sucursal] [varchar](6) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Nombre1] [varchar](30) NOT NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Nombre2] [varchar](30) NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Apellido1] [varchar](20) NOT NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Apellido2] [varchar](20) NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Contraseña] [varchar](150) NOT NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Email] [varchar](45) NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Pregunta1] [varchar](45) NOT NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Pregunta2] [varchar](45) NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Respuesta1] [varchar](20) NOT NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Respuesta2] [varchar](20) NULL
ALTER TABLE [dbo].[Tb_Usuarios] ADD [Rol] [int] NOT NULL
 CONSTRAINT [PK__Tb_Usuar__D6F931E4C0F159EF] PRIMARY KEY CLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_ventas]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_ventas](
	[Codigo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Total] [float] NOT NULL,
	[iva] [float] NOT NULL,
	[Cliente] [int] NOT NULL,
	[Sucursal] [varchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Tb_Abonos]  WITH CHECK ADD  CONSTRAINT [Fk_CreditoAbono] FOREIGN KEY([Credito])
REFERENCES [dbo].[Tb_Creditos] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Abonos] CHECK CONSTRAINT [Fk_CreditoAbono]
GO
ALTER TABLE [dbo].[Tb_AbonosCuotas]  WITH CHECK ADD FOREIGN KEY([Cuotas])
REFERENCES [dbo].[Tb_Cuotas] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Clientes]  WITH CHECK ADD  CONSTRAINT [fk_doc] FOREIGN KEY([Tipo_Documento_Codigo])
REFERENCES [dbo].[Tb_Tipo_Documento] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Clientes] CHECK CONSTRAINT [fk_doc]
GO
ALTER TABLE [dbo].[Tb_Creditos]  WITH CHECK ADD  CONSTRAINT [fk_Cre_e] FOREIGN KEY([Estado])
REFERENCES [dbo].[Tb_Estados] ([Id])
GO
ALTER TABLE [dbo].[Tb_Creditos] CHECK CONSTRAINT [fk_Cre_e]
GO
ALTER TABLE [dbo].[Tb_Creditos]  WITH CHECK ADD  CONSTRAINT [fk_Cre_v] FOREIGN KEY([Venta])
REFERENCES [dbo].[Tb_ventas] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Creditos] CHECK CONSTRAINT [fk_Cre_v]
GO
ALTER TABLE [dbo].[Tb_Cuotas]  WITH CHECK ADD FOREIGN KEY([Financiacion])
REFERENCES [dbo].[Tb_Financiacion] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Cuotas]  WITH CHECK ADD  CONSTRAINT [fk_Cuo_f] FOREIGN KEY([Financiacion])
REFERENCES [dbo].[Tb_Financiacion] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Cuotas] CHECK CONSTRAINT [fk_Cuo_f]
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada]  WITH CHECK ADD  CONSTRAINT [Fk_D_entrada] FOREIGN KEY([Entrada])
REFERENCES [dbo].[Tb_Entradas] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada] CHECK CONSTRAINT [Fk_D_entrada]
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada]  WITH CHECK ADD  CONSTRAINT [Fk_D_ProductoSucursal] FOREIGN KEY([ProductoSucursal])
REFERENCES [dbo].[Tb_Detalle_Producto_Sucursal] ([codigo_detalle])
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada] CHECK CONSTRAINT [Fk_D_ProductoSucursal]
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada]  WITH CHECK ADD  CONSTRAINT [Fk_D_Sucursal] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Detalle_Entrada] CHECK CONSTRAINT [Fk_D_Sucursal]
GO
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal]  WITH CHECK ADD  CONSTRAINT [Fk_producto_detalle] FOREIGN KEY([Producto])
REFERENCES [dbo].[Tb_Productos] ([Codigo_producto])
GO
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal] CHECK CONSTRAINT [Fk_producto_detalle]
GO
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal]  WITH CHECK ADD  CONSTRAINT [Fk_sucursal_detalle] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Detalle_Producto_Sucursal] CHECK CONSTRAINT [Fk_sucursal_detalle]
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados]  WITH CHECK ADD  CONSTRAINT [Fk_ProductoTrasl] FOREIGN KEY([ProductoSucursal])
REFERENCES [dbo].[Tb_Detalle_Producto_Sucursal] ([codigo_detalle])
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados] CHECK CONSTRAINT [Fk_ProductoTrasl]
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados]  WITH CHECK ADD  CONSTRAINT [Fk_SucursalDestino] FOREIGN KEY([Sucursal_Destino])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados] CHECK CONSTRAINT [Fk_SucursalDestino]
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados]  WITH CHECK ADD  CONSTRAINT [Fk_SucursalOrigen] FOREIGN KEY([Sucursal_Origen])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_DetalleTranslados] CHECK CONSTRAINT [Fk_SucursalOrigen]
GO
ALTER TABLE [dbo].[Tb_DetalleVenta]  WITH CHECK ADD  CONSTRAINT [fk_DetV_Ven] FOREIGN KEY([Venta])
REFERENCES [dbo].[Tb_ventas] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_DetalleVenta] CHECK CONSTRAINT [fk_DetV_Ven]
GO
ALTER TABLE [dbo].[Tb_DetalleVenta]  WITH CHECK ADD  CONSTRAINT [Fk_ProductoVenta] FOREIGN KEY([ProductoSucursal])
REFERENCES [dbo].[Tb_Detalle_Producto_Sucursal] ([codigo_detalle])
GO
ALTER TABLE [dbo].[Tb_DetalleVenta] CHECK CONSTRAINT [Fk_ProductoVenta]
GO
ALTER TABLE [dbo].[Tb_Dian]  WITH CHECK ADD  CONSTRAINT [fk_Dian] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Dian] CHECK CONSTRAINT [fk_Dian]
GO
ALTER TABLE [dbo].[Tb_Entradas]  WITH CHECK ADD  CONSTRAINT [fk_Pro_e] FOREIGN KEY([Proveedor])
REFERENCES [dbo].[Tb_Proveedores] ([Identificacion])
GO
ALTER TABLE [dbo].[Tb_Entradas] CHECK CONSTRAINT [fk_Pro_e]
GO
ALTER TABLE [dbo].[Tb_Entradas]  WITH CHECK ADD  CONSTRAINT [fk_suc_e] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Entradas] CHECK CONSTRAINT [fk_suc_e]
GO
ALTER TABLE [dbo].[Tb_Financiacion]  WITH CHECK ADD  CONSTRAINT [fk_Fin_e] FOREIGN KEY([Estado])
REFERENCES [dbo].[Tb_Estados] ([Id])
GO
ALTER TABLE [dbo].[Tb_Financiacion] CHECK CONSTRAINT [fk_Fin_e]
GO
ALTER TABLE [dbo].[Tb_Financiacion]  WITH CHECK ADD  CONSTRAINT [fk_Fin_v] FOREIGN KEY([Venta])
REFERENCES [dbo].[Tb_ventas] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Financiacion] CHECK CONSTRAINT [fk_Fin_v]
GO
ALTER TABLE [dbo].[Tb_Permiso_Denegado_Roles]  WITH CHECK ADD  CONSTRAINT [Fk_Permiso] FOREIGN KEY([PermisoID])
REFERENCES [dbo].[Tb_Permiso] ([Permiso_id])
GO
ALTER TABLE [dbo].[Tb_Permiso_Denegado_Roles] CHECK CONSTRAINT [Fk_Permiso]
GO
ALTER TABLE [dbo].[Tb_Permiso_Denegado_Roles]  WITH CHECK ADD  CONSTRAINT [Fk_Rol] FOREIGN KEY([RolID])
REFERENCES [dbo].[Tb_Rol_Usuarios] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Permiso_Denegado_Roles] CHECK CONSTRAINT [Fk_Rol]
GO
ALTER TABLE [dbo].[Tb_Post]  WITH CHECK ADD  CONSTRAINT [fk_Post] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Post] CHECK CONSTRAINT [fk_Post]
GO
ALTER TABLE [dbo].[Tb_Productos]  WITH CHECK ADD  CONSTRAINT [fk_tipo_producto] FOREIGN KEY([Tipo_Producto])
REFERENCES [dbo].[Tb_Tipo_Producto] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Productos] CHECK CONSTRAINT [fk_tipo_producto]
GO
ALTER TABLE [dbo].[Tb_Proveedores]  WITH CHECK ADD  CONSTRAINT [fk_doc_p] FOREIGN KEY([Tipo_Documento])
REFERENCES [dbo].[Tb_Tipo_Documento] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Proveedores] CHECK CONSTRAINT [fk_doc_p]
GO
ALTER TABLE [dbo].[Tb_Recuperar_contraseña]  WITH CHECK ADD  CONSTRAINT [fk_Rec] FOREIGN KEY([Usuario])
REFERENCES [dbo].[Tb_Usuarios] ([Identificacion])
GO
ALTER TABLE [dbo].[Tb_Recuperar_contraseña] CHECK CONSTRAINT [fk_Rec]
GO
ALTER TABLE [dbo].[Tb_Usuarios]  WITH CHECK ADD  CONSTRAINT [fk_doc_u] FOREIGN KEY([Tipo_Documento])
REFERENCES [dbo].[Tb_Tipo_Documento] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Usuarios] CHECK CONSTRAINT [fk_doc_u]
GO
ALTER TABLE [dbo].[Tb_Usuarios]  WITH CHECK ADD  CONSTRAINT [fk_rol_u] FOREIGN KEY([Rol])
REFERENCES [dbo].[Tb_Rol_Usuarios] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Usuarios] CHECK CONSTRAINT [fk_rol_u]
GO
ALTER TABLE [dbo].[Tb_Usuarios]  WITH CHECK ADD  CONSTRAINT [fk_suc_u] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_Usuarios] CHECK CONSTRAINT [fk_suc_u]
GO
ALTER TABLE [dbo].[Tb_ventas]  WITH CHECK ADD  CONSTRAINT [fk_Cli_v] FOREIGN KEY([Cliente])
REFERENCES [dbo].[Tb_Clientes] ([Identificacion])
GO
ALTER TABLE [dbo].[Tb_ventas] CHECK CONSTRAINT [fk_Cli_v]
GO
ALTER TABLE [dbo].[Tb_ventas]  WITH CHECK ADD  CONSTRAINT [fk_Suc_v] FOREIGN KEY([Sucursal])
REFERENCES [dbo].[Tb_Sucursales] ([Codigo])
GO
ALTER TABLE [dbo].[Tb_ventas] CHECK CONSTRAINT [fk_Suc_v]
GO
/****** Object:  StoredProcedure [dbo].[Activos]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Activos]
as
select * from Tb_Clientes where Estado<>'Inactivo'

GO
/****** Object:  StoredProcedure [dbo].[ActProveedor]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ActProveedor]
(
@id int,
@Cel Varchar(20),
@Tel varchar(20),
@TipoDoc int,
@Direccion varchar(45),
@Nit int,
@Nom1 varchar(30),
@Nom2 varchar(30),
@Apell1 varchar(20),
@Apell2 varchar(20),
@Empresa Varchar(20),
@Estado Varchar(10),
@Email varchar(45)
)
as 
update Tb_Proveedores set Celular=@Cel, Telefono=@Tel,Tipo_Documento=@TipoDoc, Direccion=@Direccion, Nit=@Nit, Nombre1=@Nom1, Nombre2=@Nom2,Apellido1=@Apell1,Apellido2=@Apell2, Empresa=@Empresa, Estado=@Estado, Email=@Email
where Identificacion=@id;




GO
/****** Object:  StoredProcedure [dbo].[ActSucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ActSucursal]
(
@Codigo int,
@Nombre varchar(20),
@Apodo varchar (15),
@Prefijo varchar(4),
@Telefono varchar(45),
@Direccion varchar(45),
@NIT varchar (15)
)
as
update Tb_Sucursales set  Nombre=@Nombre,Apodo=@Apodo,Prefijo=@Prefijo,Telefono=@Telefono, Direccion=@Direccion, NIT=@NIT
where Codigo=@Codigo;




GO
/****** Object:  StoredProcedure [dbo].[ActualizarAbono]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ActualizarAbono]
(
@codigo int,
@estado varchar(20)
)
as
update Tb_Abonos set estado = @estado where Codigo = @codigo


GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ActualizarCliente]
(
@Identificacion bigint,
@Nombre1 varchar(30),
@Nombre2 varchar (30),
@Apellido1 varchar(20),
@Apellido2 varchar(20),
@Telefono varchar(20),
@Celular varchar (20),
@Email varchar(50),
@Estado varchar(15),
@Tipo varchar(15),
@Direccion varchar(45),
@Ciudad varchar(20),
@Cupo_activo Char(1),
@Solicitud varchar(20),
@Tipo_Documento_Codigo int
)
AS
update Tb_clientes set  Nombre1 = @Nombre1, Nombre2 = @Nombre2, Apellido1 = @Apellido1, Apellido2 = @Apellido2, Telefono = @Telefono, Celular = @Celular, Email = @Email, Estado = @Estado, TipoCliente = @Tipo, Direccion = @Direccion, Ciudad = @Ciudad, Cupo_activo = @Cupo_activo, Solicitud = @Solicitud
where (Identificacion = @Identificacion)and(Tipo_Documento_Codigo = @Tipo_Documento_Codigo)



GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstado]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ActualizarEstado]
(
@id int,
@Nombre varchar(45)
)
as
update Tb_Estados set Estado=@Nombre
where Id=@id;




GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ActualizarProducto]
(
@CodigoProducto varchar(20),
@Ref Varchar(5),
@Desc varchar(30),
@Estado Varchar(15),
@TipoProd varchar(10)
)
as
declare @descTip varchar(30)
set @descTip=(select Nombre from Tb_Tipo_Producto tp where tp.Codigo = @TipoProd)
update Tb_Productos set  Descripcion= @descTip+ ' ' + @Desc, Estado=@Estado,Tipo_Producto=@TipoProd
where Codigo_producto=@CodigoProducto;




GO
/****** Object:  StoredProcedure [dbo].[ActualizarTipoProd]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ActualizarTipoProd]
(
@cod int,
@Nombre varchar(25)
)
as
update Tb_Tipo_Producto set Nombre=@Nombre
where Codigo=@cod;




GO
/****** Object:  StoredProcedure [dbo].[Anular_Credito]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Anular_Credito](
@codigo int
)
as
update Tb_Creditos set Estado = 2 where Codigo = @codigo





GO
/****** Object:  StoredProcedure [dbo].[cambiar_estado_cliente]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cambiar_estado_cliente]
(
@id bigint
)
as
if((Select Estado from Tb_Clientes where Identificacion=@id)='Activo')
begin
update Tb_Clientes set Estado = 'Inactivo' where Identificacion=@id
end
else 
begin 
update Tb_Clientes set Estado = 'Activo' where Identificacion=@id
end

GO
/****** Object:  StoredProcedure [dbo].[Cargar_Dian]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[Cargar_Dian]
(
@Sucursal int
)
as
Select Num_actual from Tb_Dian where Sucursal = @Sucursal
Update Tb_Dian set Num_actual = (Select Num_actual from Tb_Dian where Sucursal = @Sucursal) where Sucursal = @Sucursal

GO
/****** Object:  StoredProcedure [dbo].[Cargar_Iva]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Cargar_Iva]
as
select Iva from Tb_Configuraciones

GO
/****** Object:  StoredProcedure [dbo].[Consultar_Credito]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Consultar_Credito]
(
@estado int 
)
as 
select * from Tb_Creditos



GO
/****** Object:  StoredProcedure [dbo].[ConsultarClientes]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ConsultarClientes]
 (
 @Identificacion int,
 @Nombre1 varchar(30)
 )
 As
 if((@Identificacion is null) and (@Nombre1 is null))begin
 select * from Tb_Clientes
 end
else
begin
Select * from Tb_Clientes
 where Identificacion = @Identificacion or Nombre1 =@Nombre1
 end


GO
/****** Object:  StoredProcedure [dbo].[ConsultarProveedor]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ConsultarProveedor]
(
@ident int,
@nom1 varchar(30),
@empresa varchar(20)
)
as
 if((@ident is null) and (@nom1 is null)and(@empresa is null))begin
 select * from Tb_Proveedores
 end
else
begin
Select * from Tb_Proveedores
 where Identificacion=@ident or Nombre1=@nom1 or Empresa=@empresa
 end




GO
/****** Object:  StoredProcedure [dbo].[EliminarRol]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EliminarRol]
(
@codigo int
)
as
delete from Tb_Rol_Usuarios where Codigo=@codigo;




GO
/****** Object:  StoredProcedure [dbo].[EliminarSucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[EliminarSucursal]
(
@codigo int
)
as
delete from Tb_Sucursales where Codigo = @codigo;




GO
/****** Object:  StoredProcedure [dbo].[EliminarTipoDoc]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[EliminarTipoDoc]
(
@codigo int
)
as
delete from Tb_Tipo_Documento where Codigo = @codigo;



GO
/****** Object:  StoredProcedure [dbo].[Estado]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Estado]
(
@estado int 
)
as 
select * from Tb_Estados



GO
/****** Object:  StoredProcedure [dbo].[InsertarClientes]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertarClientes]
(
@Identificacion int,
@Nombre1 varchar(30),
@nombre2 varchar (30),
@Apellido1 varchar(20),
@Apellido2 varchar(20),
@Telefono varchar(20),
@Celular varchar (20),
@Email varchar(50),
@Estado varchar(15),
@Tipo varchar(15),
@Direccion varchar(45),
@Ciudad varchar(20),
@Cupo_activo Char(1),
@Solicitud varchar(20),
@Tipo_Documento_Codigo int
)

AS

INSERT INTO Tb_Clientes (Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Telefono, Celular, Email, Estado, TipoCliente, Direccion, Ciudad, Cupo_activo, Solicitud, Tipo_Documento_Codigo)
VALUES (@Identificacion, @Nombre1, @Nombre2, @Apellido1, @Apellido2, @Telefono, @Celular, @Email, @Estado, @Tipo, @Direccion, @Ciudad, @Cupo_activo, @Solicitud, @Tipo_Documento_Codigo)



GO
/****** Object:  StoredProcedure [dbo].[Llamar_Sucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Llamar_Sucursal]
(
@user varchar (30)
)
as
select Sucursal from Tb_Usuarios where Email = @user
GO
/****** Object:  StoredProcedure [dbo].[llenarp]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[llenarp]
(
@user varchar(30)
)
as
Select p.Codigo_producto,p.Descripcion from tb_productos p join Tb_Detalle_Producto_Sucursal d on (p.Codigo_producto = d.Producto) where p.Estado='Activo' and d.Sucursal = (Select Sucursal from Tb_Usuarios where Email = @user)





GO
/****** Object:  StoredProcedure [dbo].[Precio]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Precio]
(
@producto varchar(30),
@Sucursal int
)
as
select Valor_Venta from Tb_Detalle_Producto_Sucursal where Producto = @producto and Sucursal = @Sucursal

GO
/****** Object:  StoredProcedure [dbo].[Reg_Credito]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Reg_Credito](
@codigo int,
@fecha date,
@estado int,
@num_pagare int,
@cuota_inicial int,
@total_adeudado float,
@venta int
)
AS
if ((Select TipoCliente from Tb_Clientes c join Tb_ventas ve on (c.Identificacion = ve.Cliente) where ve.Codigo = @venta) = 'Mayorista')
begin
if ((select Total from Tb_ventas where(Codigo = @venta)) <= (select MontoMayorista from Tb_Configuraciones)) 
	begin
		insert into Tb_Creditos(Codigo,Fecha,Estado,Numero_Pagare,Cuota_inicial,Total_Adeudado,Venta) 
		values(@codigo,@fecha,@estado,@num_pagare,@cuota_inicial,@total_adeudado,@venta)
		
	end
end










GO
/****** Object:  StoredProcedure [dbo].[Reg_Financiacion]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Reg_Financiacion](
@codigo int,
@fecha date,
@estado int,
@num_pagare int,
@tiempo varchar(20),
@aumento float,
@total float,
@Cuota_inicial float,
@Total_Adeudado float,
@venta int
)
AS
if ((Select TipoCliente from Tb_Clientes c join Tb_ventas ve on (c.Identificacion = ve.Cliente) where ve.Codigo = @venta) = 'Minorista')
begin
if ((select Total from Tb_ventas where(Codigo = @venta)) <= (select MontoMinorista from Tb_Configuraciones)) 
	begin
		insert into Tb_Financiacion values(@codigo,@fecha,@estado,@num_pagare,@tiempo,@aumento,@total,@Cuota_inicial,@Total_Adeudado,@venta)
	end
end




GO
/****** Object:  StoredProcedure [dbo].[RegDetalleEntrada]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegDetalleEntrada]
(
@Cantidad int,
@Precio float,
@Entrada int,
@Sucursal varchar(6),
@Producto Varchar (20)
)
as
insert into Tb_Detalle_Entrada values(@Cantidad,@Precio,@Entrada,@Sucursal,@Producto);




GO
/****** Object:  StoredProcedure [dbo].[RegDetalleProdSucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegDetalleProdSucursal]
(
@codigo_detalle varchar(20),
@StockMin int,
@StockMax int,
@ValorVenta float,
@ValorMayor float,
@ValorEspecial float,
@Cantidad int,
@Producto varchar (10),
@Sucursal varchar(6)
)
as
insert into Tb_Detalle_Producto_Sucursal values(@codigo_detalle, @StockMin,@StockMax,@ValorVenta,@ValorMayor,@ValorEspecial,@Cantidad,@Producto,@Sucursal);




GO
/****** Object:  StoredProcedure [dbo].[RegDetalleTraslado]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegDetalleTraslado]
(
@Cantidad int,
@Total float,
@Fecha varchar(45),
@Aumento float,
@SucursalOrigen varchar(6),
@Producto varchar(20),
@SucursalDestino varchar(6)
)
as
insert into Tb_DetalleTranslados values(@Cantidad,@Total,@Fecha,@Aumento,@SucursalOrigen,@Producto,@SucursalDestino);




GO
/****** Object:  StoredProcedure [dbo].[RegDetalleVenta]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegDetalleVenta]
(
@Cantidad int,
@Descuento float,
@SubTotal float,
@Venta int,
@precio float,
@Producto varchar(20)
)
as
insert into Tb_DetalleVenta values(@Cantidad,@Descuento,@SubTotal,@Venta,@Producto,@precio);




GO
/****** Object:  StoredProcedure [dbo].[Registrar_detalle_producto_sucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Registrar_detalle_producto_sucursal]
(

@stock_min int,
@stock_max int,
@valor_venta float,
@valor_mayor float,
@valor_especial float,
@cantidad int,
@producto varchar(10),
@sucursal varchar(6)
)
as
insert into Tb_Detalle_Producto_Sucursal values ((@producto + @sucursal),@stock_min,@stock_max,@valor_venta,@valor_mayor,
@valor_especial,@cantidad,@producto,@sucursal)

GO
/****** Object:  StoredProcedure [dbo].[Registrar_producto]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Registrar_producto]
(

@Referencia varchar(5),
@descripcion varchar(100),
@estado varchar(15),
@Tipo_producto varchar(10),
@Cantidad int
)
as
declare @descTipo varchar(30)
set @descTipo =(select Nombre from Tb_Tipo_Producto tp where tp.Codigo=@Tipo_producto)
insert into Tb_Productos values((@Tipo_producto + @Referencia),@Referencia,(@descTipo+' ' + @descripcion),@estado,@Tipo_producto, @Cantidad)




GO
/****** Object:  StoredProcedure [dbo].[registrarAbono]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[registrarAbono]
(
@cod int,
@fecha date,
@credito int,
@valor float,
@estado varchar(20)
)
as
insert into Tb_Abonos values(@cod, @fecha,@credito,@valor,@estado);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarDian]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[RegistrarDian]
(
@Codigo int,
@Resolucion varchar(30),
@FechaInicio Date,
@FechaFin Date,
@NumInicio int,
@NumFin int,
@Sucursal varchar(6),
@Estado varchar(12)
)
as
insert into Tb_Dian values(@Codigo,@Resolucion,@FechaInicio,@FechaFin,@NumInicio,@NumFin,(@NumInicio - 1),@Sucursal,@Estado);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarEntrada]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarEntrada]
(
@Codigo int,
@Fecha Date,
@Total float,
@Estado varchar(10),
@Sucursal varchar(6),
@Proveedor int
)
as
insert into Tb_Entradas values(@Codigo,@Fecha,@Total,@Estado,@Sucursal,@Proveedor);



GO
/****** Object:  StoredProcedure [dbo].[RegistrarEstado]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarEstado]
(
@id int,
@Nombre varchar(45)
)
as
insert into Tb_Estados values(@id,@Nombre);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarPost]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarPost]
(
@codigo int,
@NumActual int,
@Resolucion varchar(30),
@Sucursal varchar(6)
)
as
insert into Tb_Post values(@codigo,@NumActual,@Resolucion,@Sucursal);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarRol]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarRol]
(
@codigo int,
@nombre varchar(45)
)
as
insert into Tb_Rol_Usuarios values(@codigo,@nombre);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarTipoProd]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarTipoProd]
(
@cod int,
@Nombre varchar(25)
)
as
insert into Tb_Tipo_Producto values(@cod,@Nombre);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarUsuario]
(
@id int,
@TipoDoc int,
@Sucursal int,
@Nom1 varchar(30),
@Nom2 varchar(30),
@Apell1 varchar(20),
@Apell2 varchar(20),
@Contr varchar(45),
@Email Varchar(45),
@Preg1 Varchar(45),
@Preg2 Varchar(45),
@Resp1 Varchar(20),
@Resp2 Varchar(20),
@Rol int
)
as
insert into Tb_Usuarios values(@id,@TipoDoc,@Sucursal,@Nom1,@Nom2,@Apell1,@Apell2,@Contr,@Email,@Preg1,@Preg2,@Resp1,@Resp2,@Rol);




GO
/****** Object:  StoredProcedure [dbo].[RegistrarVenta]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegistrarVenta]
(
@Codigo int,
@Fecha date,
@Total float,
@iva float,
@Cliente int,
@Sucursal varchar(6)
)
as
insert into Tb_ventas values(@Codigo,@Fecha,@Total,@iva,@Cliente,@Sucursal);



GO
/****** Object:  StoredProcedure [dbo].[RegProveedor]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegProveedor]
(
@id int,
@Cel Varchar(20),
@Tel varchar(20),
@TipoDoc int,
@Direccion varchar(45),
@Nit int,
@Nom1 varchar(30),
@Nom2 varchar(30),
@Apell1 varchar(20),
@Apell2 varchar(20),
@Empresa Varchar(20),
@Estado Varchar(10),
@Email varchar(45)
)
as insert into Tb_Proveedores values(@id,@Cel,@Tel,@TipoDoc,@Direccion,@Nit,@Nom1,@Nom2,@Apell1,@Apell2,@Empresa,@Estado,@Email);


GO
/****** Object:  StoredProcedure [dbo].[RegSucursal]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegSucursal]
(
@Codigo varchar(6),
@Nombre varchar(20),
@Apodo varchar (15),
@Prefijo varchar(4),
@Telefono varchar(45),
@Direccion varchar(45),
@NIT varchar (15)
)
as
insert into Tb_Sucursales values(@Codigo,@Nombre,@Apodo,@Prefijo,@Telefono,@Direccion,@NIT);




GO
/****** Object:  StoredProcedure [dbo].[RegTipoDoc]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[RegTipoDoc]
(
@codigo int,
@nombre varchar(20)
)
as
insert into Tb_Tipo_Documento values(@codigo,@nombre);




GO
/****** Object:  StoredProcedure [dbo].[tipo_doc]    Script Date: 24/05/2017 20:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[tipo_doc]
As
select * from Tb_Tipo_Documento





GO
