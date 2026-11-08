import json
import math
import random
import sys
import urllib.request

base = sys.argv[1] if len(sys.argv) > 1 else "http://localhost:5000"

random.seed(7)
TOTAL = 45

# (r_min, r_max, pes, taxa d'encert)
ZONES = [
    (0.03, 0.20, 0.30, 0.65),
    (0.20, 0.45, 0.35, 0.42),
    (0.45, 0.65, 0.35, 0.33),
]


def genera_tir():
    r_min, r_max, _, taxa = random.choices(ZONES, weights=[z[2] for z in ZONES])[0]
    while True:
        r = random.uniform(r_min, r_max)
        angle = random.uniform(0, math.pi)
        x = 0.5 + r * math.cos(angle)
        y = 0.1 + r * math.sin(angle)
        if 0 <= x <= 1 and 0 <= y <= 1:
            return {"x": round(x, 3), "y": round(y, 3), "made": random.random() < taxa}


def envia(tir):
    peticio = urllib.request.Request(
        base + "/shots",
        data=json.dumps(tir).encode(),
        headers={"Content-Type": "application/json"},
        method="POST",
    )
    urllib.request.urlopen(peticio).read()


for _ in range(TOTAL):
    envia(genera_tir())

print(f"{TOTAL} tirs enviats.")
with urllib.request.urlopen(base + "/stats") as resposta:
    for zona in json.load(resposta):
        print(zona["zone"], zona["attempts"], zona["made"], f'{zona["percentage"]}%')