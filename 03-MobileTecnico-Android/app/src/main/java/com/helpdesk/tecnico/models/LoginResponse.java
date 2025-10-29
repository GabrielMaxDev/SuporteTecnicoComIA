package com.helpdesk.tecnico.models;

public class LoginResponse {
    private boolean sucesso;
    private String token;
    private String mensagem;

    public boolean isSucesso() { return sucesso; }
    public void setSucesso(boolean sucesso) { this.sucesso = sucesso; }

    public String getToken() { return token; }
    public void setToken(String token) { this.token = token; }

    public String getMensagem() { return mensagem; }
    public void setMensagem(String mensagem) { this.mensagem = mensagem; }
}
