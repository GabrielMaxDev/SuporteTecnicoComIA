package com.helpdesk.tecnico.activities;

import android.os.Bundle;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;
import com.helpdesk.tecnico.R;
import com.helpdesk.tecnico.models.Chamado;
import com.helpdesk.tecnico.network.ApiClient;
import com.helpdesk.tecnico.network.ApiService;
import java.util.ArrayList;
import java.util.List;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private SwipeRefreshLayout swipeRefresh;
    private ApiService apiService;
    private List<Chamado> chamados = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        recyclerView = findViewById(R.id.recyclerViewChamados);
        swipeRefresh = findViewById(R.id.swipeRefresh);

        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        apiService = ApiClient.getClient().create(ApiService.class);

        swipeRefresh.setOnRefreshListener(this::carregarChamados);

        carregarChamados();
    }

    private void carregarChamados() {
        swipeRefresh.setRefreshing(true);

        Call<List<Chamado>> call = apiService.getChamadosAtribuidos();
        call.enqueue(new Callback<List<Chamado>>() {
            @Override
            public void onResponse(Call<List<Chamado>> call, Response<List<Chamado>> response) {
                swipeRefresh.setRefreshing(false);

                if (response.isSuccessful() && response.body() != null) {
                    chamados = response.body();
                    // TODO: Atualizar adapter do RecyclerView
                    Toast.makeText(MainActivity.this, 
                        chamados.size() + " chamados carregados", Toast.LENGTH_SHORT).show();
                } else {
                    Toast.makeText(MainActivity.this, 
                        "Erro ao carregar chamados", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<Chamado>> call, Throwable t) {
                swipeRefresh.setRefreshing(false);
                Toast.makeText(MainActivity.this, 
                    "Erro: " + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
}
