package com.helpdesk.tecnico.models;

import java.util.Date;

public class Chamado {
    private int id;
    private String titulo;
    private String descricao;
    private String categoria;
    private String prioridade;
    private String status;
    private Date dataAbertura;
    private String nomeCliente;
    private String sugestaoIA;

    // Getters e Setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getTitulo() { return titulo; }
    public void setTitulo(String titulo) { this.titulo = titulo; }

    public String getDescricao() { return descricao; }
    public void setDescricao(String descricao) { this.descricao = descricao; }

    public String getCategoria() { return categoria; }
    public void setCategoria(String categoria) { this.categoria = categoria; }

    public String getPrioridade() { return prioridade; }
    public void setPrioridade(String prioridade) { this.prioridade = prioridade; }

    public String getStatus() { return status; }
    public void setStatus(String status) { this.status = status; }

    public Date getDataAbertura() { return dataAbertura; }
    public void setDataAbertura(Date dataAbertura) { this.dataAbertura = dataAbertura; }

    public String getNomeCliente() { return nomeCliente; }
    public void setNomeCliente(String nomeCliente) { this.nomeCliente = nomeCliente; }

    public String getSugestaoIA() { return sugestaoIA; }
    public void setSugestaoIA(String sugestaoIA) { this.sugestaoIA = sugestaoIA; }
}
