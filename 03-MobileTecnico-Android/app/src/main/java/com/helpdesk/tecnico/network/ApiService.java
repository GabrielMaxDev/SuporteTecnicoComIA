package com.helpdesk.tecnico.network;

import com.helpdesk.tecnico.models.Chamado;
import com.helpdesk.tecnico.models.LoginRequest;
import com.helpdesk.tecnico.models.LoginResponse;
import java.util.List;
import retrofit2.Call;
import retrofit2.http.*;

public interface ApiService {

    @POST("auth/login")
    Call<LoginResponse> login(@Body LoginRequest request);

    @GET("chamados/atribuidos")
    Call<List<Chamado>> getChamadosAtribuidos();

    @GET("chamados/{id}")
    Call<Chamado> getChamado(@Path("id") int id);

    @PUT("chamados/{id}/status")
    Call<Void> atualizarStatus(@Path("id") int id, @Body StatusUpdate status);

    class StatusUpdate {
        public String status;
        public StatusUpdate(String status) {
            this.status = status;
        }
    }
}
