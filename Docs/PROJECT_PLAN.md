# The Developer — Product Plan

버전: 0.2
기준일: 2026-09-11
상태: 현행 PL 기준 / M0 팀 검토 대상

이 문서는 `무엇을 왜 만드는가`를 정의한다. 구현 순서와 일정은 [milestones-and-core-design.md](milestones-and-core-design.md), 담당 경계는 [TEAM_ROLE_OWNERSHIP.md](TEAM_ROLE_OWNERSHIP.md)를 따른다.

## 1. 게임 정체성

> 플레이어가 직접 전투하면서, 선발대가 행성에 미리 설치한 수많은 터렛 중 일부만 제한된 전력으로 가동하고, Mission이 진행될수록 확장되는 전장을 방어하는 2D 탑다운 액션 타워디펜스.

터렛은 플레이어가 건설하거나 이동하지 않는다. `설치`는 제작자가 Scene/Prefab에 미리 배치한다는 뜻이며, 플레이어 행동은 터렛 선택·가동·정지와 전력 재배치다.

## 2. 플레이어 경험 목표

1. 모든 터렛을 켤 수 없다는 긴장감을 느낀다.
2. 적 조합과 공격 방향을 읽고 전력을 배분한다.
3. 전력이 없는 방어선으로 직접 이동해 빈틈을 메운다.
4. 새 Sector가 열리면서 새로운 선택지를 발견한다.
5. 확장과 함께 전선·이동 거리·위협도 커지는 부담을 감수한다.
6. 마지막에는 자신이 구성한 방어망 전체로 행성을 지켜냈다는 성취감을 얻는다.

## 3. Core Loop와 Meta Loop

```text
Spaceship에서 Mission·행성 상태 확인
→ Research·Power·Turret 준비
→ Planet으로 Launch
→ Wave·공격 방향 확인
→ 제한 전력 안에서 터렛 가동 조합 선택
→ 플레이어 직접 전투와 전력 재배치
→ Control Unit 방어 및 Mission 완료
→ Sector Expansion·보상 획득
→ Spaceship으로 Return
→ Result 확인·성장 적용·다음 Mission 선택
```

### 승리 조건

- 해당 Mission의 필수 Wave 또는 목표를 완료한다.
- Planet 1 마지막 Mission에서는 Boss를 처치한다.

### 실패 조건과 보상

- Player 또는 Control Unit HP가 0이면 실패한다.
- 실패해도 낮은 비율의 재화를 지급한다. 정확한 비율은 `ModeRules`와 Balance Data에서 결정한다.
- 보상은 `RewardReceipt`를 통해 Profile에 반영하며, Release의 최종 권한은 Backend가 가진다.

## 4. 핵심 게임 규칙

### 4.1 Power Budget

- 최대 전력은 공간에 펼쳐진 Loadout Cost다.
- Pre-Wave에는 자유롭게 조합을 변경한다.
- In-Wave 변경은 가능하지만 전력 회수 지연 또는 횟수 제한을 둔다.
- 하나의 터렛 조합이 모든 Wave의 정답이 되어서는 안 된다.

초기 Paper Simulation 기준값은 최대 100, Cannon 20, Missile 30, Laser 50이다. 이는 확정 Balance가 아니다.

### 4.2 Map Expansion

확장마다 아래 두 항목을 모두 제공한다.

- 새 이점: 신규 터렛, 외곽 요격선, 지름길, Radar, 보조 시설 중 하나 이상
- 새 위협: 신규 Spawn 방향, 긴 이동 거리, 다중 전선, 특수 Enemy 중 하나 이상

면적과 배경만 늘어나는 확장은 Content로 인정하지 않는다. 이전 Sector의 터렛은 확장 후에도 내곽 방어선으로 유효해야 한다.

### 4.3 Player와 Turret

- 터렛만 켜고 기다리는 플레이와 Player 화력만으로 전력 규칙을 무시하는 플레이를 모두 피한다.
- Player는 이동 가능한 화력이며 터렛 사각, 긴급 Enemy, 전력이 없는 구역을 담당한다.
- Turret Cost·Range·역할·현재 상태와 Enemy의 주요 특성은 전투 중 읽을 수 있어야 한다.

### 4.4 Spaceship Hub

Spaceship은 장식 Lobby가 아니라 Mission 준비와 성장 결과를 이해하는 Gameplay Space다.

- Navigation: 행성 상태·Expansion·Mission 선택
- Engineering/Research: 전력 또는 터렛 성장 적용
- Communications: 선발대 기록과 Mission 정보
- Launch Point: 선택한 Mission 확인 후 출격

Vertical Slice에서는 기능이 있는 작은 Interior만 만든다. 빈 복도를 오래 이동하거나 다층 함선을 구현하지 않는다.

## 5. M2 Product Vertical Slice

M1 New Core Foundation이 통과된 뒤 M2에서 제작한다.

### 플레이 범위

- 목표 플레이 시간: 15~20분
- Planet 1 Full Map 1개
- 검증 상태: 50% → 75% → 100%
- 완성형 확장 계획: 50% → 65% → 75% → 90% → 100%
- Player Weapon 1종, Control Unit 1개
- Cannon, Missile, Laser 터렛
- Default, Tanker, Assassin Enemy와 Boss 1종
- Power HUD와 개별 터렛 ON/OFF
- 소형 Spaceship Interior와 필수 Console
- Spaceship → Planet → Result → Spaceship 왕복
- Upgrade Choice 1개, Clear·Fail·Retry
- 최소 Tutorial과 Context 설명

### M2 제외 범위

- 실제 Online Co-op, Random Match, SOS
- 두 번째 Planet과 다수 Player Weapon
- 플레이어의 터렛 건설·이동
- Sector 단위 일괄 전원 제어
- 대형 Research Tree와 장기 Economy
- Random/Procedural Map
- Multi-deck Spaceship, Ship Combat, Crew/Maintenance Simulation
- 전체 Story와 장편 Cutscene

## 6. 출시 방향

- M2까지는 Single-player Core와 Product Vertical Slice를 검증한다.
- M3에서 Steam 친구 초대 기반 2-player Co-op MVP를 검증한다.
- M4에서 Random Match, SOS, Reconnect, Chat/Ping과 Content Alpha를 범위별로 검증한다.
- 실제 Host Migration은 Deferred다. M4에서 Snapshot/Authority 이전 PoC만 수행하고 출시 포함 여부는 별도 결정한다.
- PvP와 `RivalPlayerAgent`는 Release 이후 별도 Product Track이다.
- Profile은 Single·Co-op에서 공유하며 Backend authoritative를 목표로 한다. Local versioned binary는 캐시다.

## 7. Product Vertical Slice 통과 지표

- 외부 Tester 5명 중 4명이 설명 없이 터렛을 켜고 Power Limit을 이해한다.
- 5명 중 4명이 Expansion과 Power Allocation을 게임의 특징으로 기억한다.
- 5명 중 4명이 Spaceship에서 다음 행동과 Launch 방법을 찾는다.
- 서로 다른 두 Turret Build로 Clear할 수 있다.
- Player의 직접 전투가 최소 한 번 이상 승패에 의미 있게 영향을 준다.
- Expansion 뒤 Attack Direction과 Power Allocation이 실제로 달라진다.
- 이전 Sector가 최종 방어에서도 사용된다.
- Progression Blocker, Crash, Save Corruption이 없다.

## 8. 미결정 항목

다음은 정해진 기능이 아니라 검증할 질문이다.

- 모든 Sub-stage 후 Spaceship으로 귀환할지, 주요 Mission 뒤에만 귀환할지
- 이전 Mission의 터렛 상태·피해를 다음 Mission에 유지할지
- In-Wave 전력 전환의 지연과 횟수 제한
- 터렛 파괴·수리 규칙
- 미확장 구역 접근 방식
- 최종 Planet 수와 전체 플레이 시간
- Sector 일괄 전원 제어의 필요성
- Single-player Offline 정책과 Backend 장애 시 동작
- Host Migration의 출시 포함 여부

미결정 항목은 현재 Milestone의 P0/P1을 대체하지 않는다. 실험 결과와 함께 Decision Log에 승인된 뒤 Backlog로 이동한다.

## 9. Decision Log

| 날짜 | 결정 | 이유 | 영향 |
| --- | --- | --- | --- |
| 2026-09-10 | 첫 Product 목표를 Planet 1 Vertical Slice로 제한 | Core Fun 검증 전 Content 확장 방지 | Online, Planet 2, 대형 Progression 보류 |
| 2026-09-10 | 터렛은 선발대가 미리 설치 | 건설보다 제한 전력 운용에 집중 | 플레이어의 건설·이동 기능 제외 |
| 2026-09-10 | 초기 검증은 개별 터렛 조작 | 현재 Turret 수와 UI 복잡도에 적합 | Sector Control은 Playtest 후 재검토 |
| 2026-09-10 | 소형 Spaceship 왕복을 Product Vertical Slice에 포함 | Combat만으로 최종 경험을 대표할 수 없음 | M2에 Hub·Launch·Return 포함 |
| 2026-09-11 | M1을 New Core Foundation으로 정의 | 레거시 Manager 위에 신규 기능이 쌓이는 것을 방지 | 실제 Planet/Hub 통합은 M2에서 수행 |
| 2026-09-11 | Profile은 Backend authoritative, Local Binary는 cache | Single·Co-op의 공정한 공유 성장 유지 | M1은 Port/Fake, 운영 Backend는 M4 |
| 2026-09-11 | M3의 Online 목표를 친구 초대 기반 2인 Co-op으로 제한 | Online 범위 팽창 방지 | Random/SOS/Chat/Ping은 M4 |
| 2026-09-11 | Host Migration 실제 기능과 PvP를 Deferred로 유지 | M2/M3 핵심 검증 우선 | M4는 Migration PoC까지만 기본 범위 |
