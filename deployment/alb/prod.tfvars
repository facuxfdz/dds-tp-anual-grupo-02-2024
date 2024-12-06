public_subnet_name  = "acceso-alimentario-public"
domain_zone_id      = "Z00362715JGFXPFE3OAC"
vpc_name            = "acceso-alimentario"
domain_record       = "acceso-alimentario.opsconsultingservices.com"
additional_records = [
    "back.acceso-alimentario.opsconsultingservices.com",
    "recomendaciones.acceso-alimentario.opsconsultingservices.com"
]
alb_name            = "acceso-alimentario"
region              = "us-east-1"
user_pool_arn       = "arn:aws:cognito-idp:us-east-1:034781041905:userpool/us-east-1_w3tGiEOeB"
user_pool_client_id = "bufed84qqkl82j8ptd7nn3uh0"
user_pool_domain    = "auth.acceso-alimentario.opsconsultingservices.com"