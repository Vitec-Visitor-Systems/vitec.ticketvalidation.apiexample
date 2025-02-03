job "vitec-integrations-external-validator" {
  type      = "service"
  group "vitec-integrations-external-validator" {
    count = 1
    network {
      mode = "bridge"
      port "http" {
        to = 80
      }
    }
    service {
      name = "vitec-integrations-external-validator-svc"
      port = "http"
      tags = [
        "traefik.enable=true",
        "traefik.port=80",
        "traefik.http.routers.vitec-external-validator.rule=Host(`XXXXXXXX`)"
      ]
      check {
        type     = "http"
        path     = "/health"
        interval = "30s"
        timeout  = "60s"
      }
    }
    task "vitec-integrations-external-validator-task" {
      env {
        ASPNETCORE_URLS					           = "http://*:${NOMAD_PORT_http}/"
        Authentication__Type               = "OAuth2"
      }
      driver = "docker"

      resources {
        cpu    = 100
        memory = 128
      }
      config {
        image = "nexus.entryevent.se:8085/vitec.integrations.exvalidator:2025.1.1"
        ports = ["http"]
        auth {
          username = "XXXXX"
          password = "XXXXXX"
        }
      }
    }
  }
}