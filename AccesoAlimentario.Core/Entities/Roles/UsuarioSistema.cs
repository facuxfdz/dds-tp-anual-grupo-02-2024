﻿using AccesoAlimentario.Core.Entities.Personas;

namespace AccesoAlimentario.Core.Entities.Roles;

public class UsuarioSistema : Rol
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public UsuarioSistema()
    {
    }
    
    public UsuarioSistema(Persona persona, string userName, string password) : base(persona)
    {
        UserName = userName;
        Password = password;
    }

    public void Actualizar(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}