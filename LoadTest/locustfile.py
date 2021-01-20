from locust import HttpUser, task
from faker import Faker
import random


class WebsiteUser(HttpUser):

    # def __init__(self, *args, **kwargs):
    #     super().__init__(*args, **kwargs)
    #     self.client.verify = False
    #     fake = Faker()
    #     for x in range(100):
    #         json = {
    #             "birthDate": fake.date(),
    #             "height": round(random.uniform(1.5, 1.99), 2),
    #             "name": fake.name()
    #         }
    #         self.client.post("/api/v1/people", json=json)

    def on_start(self):
        self.client.verify = False

    @task
    def get(self):
        person_id = random.randint(1, 100)
        self.client.get("/api/v1/people/" + str(person_id))

    @task
    def put(self):
        fake = Faker()
        json = {
            "birthDate": fake.date(),
            "height": round(random.uniform(1.5, 1.99), 2),
            "name": fake.name()
        }
        person_id = random.randint(1, 100)
        self.client.put("/api/v1/people/" + str(person_id), json=json)

    @task
    def post(self):
        fake = Faker()
        json = {
            "birthDate": fake.date(),
            "height": round(random.uniform(1.5, 1.99), 2),
            "name": fake.name()
        }
        self.client.post(f"/api/v1/people/", json=json)
