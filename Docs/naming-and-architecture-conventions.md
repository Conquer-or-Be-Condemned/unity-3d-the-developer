# Naming and Architecture Conventions

작성일: 2026-09-08
상태: 팀 코드/문서 명명 규칙 초안 / 로컬 논의용

## 목적

이 규칙은 Unity 게임에서 널리 이해되는 Clean Architecture 및 게임플레이 용어를 사용해,
클래스의 책임과 의존성 방향이 이름만으로도 드러나게 한다. 특정 프레임워크를 강제하지 않으며,
일관성이 정확한 단어 선택보다 우선한다.

## 계층 이름

| 계층 | 책임 | 허용 의존성 | 예시 |
| --- | --- | --- | --- |
| `Bootstrap` | 앱 조립·수명주기 시작 | 모든 구체 구현 | `AppBootstrap`, `CompositionRoot` |
| `Domain` | 순수 게임 규칙·상태·값 | .NET BCL만 | `MatchState`, `GameSimulation`, `WaveState` |
| `Application` | Use Case 조율·세션 흐름·Port | Domain, Contracts | `MatchSession`, `ProfileUseCase` |
| `Contracts` | 외부 기능의 추상 Port | Domain BCL | `IProfileService`, `IContentCatalog` |
| `Infrastructure` | 외부 SDK/저장/HTTP의 구현 | Contracts, Domain, Unity/SDK | `SteamPlatformAdapter`, `BinaryProfileRepository` |
| `Content` | authoring 정의값과 Runtime 해석 | Domain, Unity authoring API | `StageDefinition`, `ContentCatalog` |
| `Presentation` | Unity Scene/UI/입력/오디오 표현 | Application, Contracts, Unity | `HudPresenter`, `PlayerSceneAdapter` |
| `Legacy` | 이전 코드 레퍼런스 | 신규 의존성 금지 | 기존 `UI & Manager` |

`Domain`과 `Application`은 Unity API를 참조하지 않는다. SDK와 MonoBehaviour는 반드시
`Infrastructure` 또는 `Presentation`에 둔다.

## 클래스 접미사 규칙

| 이름 | 언제 쓰는가 | 예시 | 피해야 할 용도 |
| --- | --- | --- | --- |
| `State` | 변경되는 현재 상태 모델 | `MatchState`, `PlayerState` | ScriptableObject 원본 데이터 |
| `Definition` | 밸런스/콘텐츠 원본 정의 | `EnemyDefinition`, `WaveDefinition` | 런타임 mutable 값 |
| `Config` | 시작 후 불변인 실행 설정 | `MatchConfig`, `LoadoutConfig` | 사용자 저장 원본 |
| `Profile` | 계정 단위 진행도/소유 데이터 | `PlayerProfile` | 한 판의 HP/웨이브 |
| `Snapshot` | 특정 시점의 직렬화 가능한 복사본 | `MatchSnapshot`, `ProfileSnapshot` | 실시간 mutable 객체 |
| `Command` | 권한 시스템에 요청하는 의도 | `StartWaveCommand`, `FireCommand` | UI 이벤트/결과 통지 |
| `Event` | 이미 발생한 사실 | `WaveStarted`, `PlayerHealthChanged` | 상태를 바꾸라는 요청 |
| `Result` | Use Case/매치의 확정 결과 | `MatchResult`, `RewardReceipt` | 현재 상태 모델 |
| `System` | 반복 가능한 게임 규칙 계산 | `WaveSystem`, `RewardSystem` | 씬/UI/SDK 조립 |
| `Service` | 외부 또는 앱 수준 기능 | `ProfileService`, `LobbyService` | 모든 책임을 가진 Manager |
| `Repository` | 데이터 읽기/쓰기 추상화 | `ProfileRepository` | HTTP 규칙/게임 규칙 |
| `Gateway` / `Client` | 원격 API 통신 경계 | `BackendGateway`, `SteamWebApiClient` | 로컬 게임 규칙 |
| `Adapter` | Unity/SDK와 Core 계약 변환 | `PlayerSceneAdapter`, `SteamPlatformAdapter` | Domain 모델 |
| `Presenter` | State/Event를 View에 표시 | `HudPresenter`, `DialoguePresenter` | 직접 게임 규칙 실행 |
| `Controller` | 입력 또는 화면 흐름 조정 | `SceneFlowController`, `InputController` | 전역 상태 저장 |
| `Factory` | 복잡한 객체 생성 | `MatchSessionFactory` | 싱글턴 접근 래퍼 |
| `Rules` | 모드별 규칙 차이 | `StoryModeRules`, `CoopModeRules` | Unity 화면 로직 |
| `Authority` | Command 검증/실행 권한 | `LocalAuthority`, `HostAuthority` | 단순 네트워크 연결 |
| `Agent` | Command를 생성하는 행위자 | `HumanPlayerAgent`, `AllyBotAgent` | HP/인벤토리 상태 |

## 표준 핵심 이름

```text
AppBootstrap            앱 시작 지점
AppRoot                 App 스코프 composition root
AppServices             Bootstrap 내부의 앱 서비스 집합
GameSession / MatchSession
                        한 번의 스테이지·던전·협동 매치 수명주기
MatchState / RunState   해당 매치의 런타임 상태
GameSimulation          순수 전투·웨이브 상태 전이
PlayerProfile           계정 진행도
ContentCatalog          Definition을 검색/해석하는 API
SceneFlowController     Hub/Lobby/Match 씬 흐름
HudPresenter             HUD 표시 갱신
```

`GameSession`과 `MatchSession`은 둘 다 통용된다. 이 프로젝트에서는 협동/미래 PvP까지
같은 모델로 표현하므로 `MatchSession`을 기본 이름으로 권장한다. `RunState`는 기존 초안의
의미가 명확하므로 Domain 내부 세부 상태 이름으로 유지할 수 있다.

## Interface와 구현체

Port는 소비자가 필요한 동작 중심으로 `I` 접두사를 쓴다. 구현체는 구현 기술을 이름에 넣는다.

```text
IProfileRepository
  ├─ BinaryProfileRepository
  └─ BackendProfileRepository

IPlatformService
  ├─ FakePlatformService
  └─ SteamPlatformService

IAuthority
  ├─ LocalAuthority
  ├─ HostAuthority
  └─ DedicatedServerAuthority
```

`IManager`, `IGameService`, `IRepositoryManager`처럼 책임이 넓은 인터페이스는 만들지 않는다.

## 상속과 조합

게임 규칙/서비스에는 상속보다 조합을 사용한다.

```text
good: MatchSession(GameSimulation, IModeRules, IMatchEvents, IClock)
bad:  BaseGameManager -> InGameManager -> CoopGameManager -> PvPGameManager
```

상속이 허용되는 위치는 Unity 표현 계층의 작은 공통 동작이다.

```text
SceneAdapterBase : MonoBehaviour
  ├─ PlayerSceneAdapter
  └─ ControlUnitSceneAdapter
```

`MonoBehaviour`는 Domain/Application의 base class가 될 수 없다.

## Namespace와 파일 규칙

```text
TeamHjd.Game.Domain
TeamHjd.Game.Application
TeamHjd.Game.Contracts
TeamHjd.Game.Content
TeamHjd.Game.Infrastructure
TeamHjd.Game.Presentation
```

- public 타입 하나당 파일 하나를 원칙으로 한다.
- 파일명은 public 타입명과 정확히 일치한다.
- enum은 값의 범주를 나타내는 명사로 쓴다: `MatchPhase`, `CurrencyType`.
- bool은 `Is`, `Has`, `Can`, `Should`로 시작한다: `IsRunning`, `CanReconnect`.
- 컬렉션은 복수형: `Players`, `Definitions`, `Commands`.
- private field는 `_camelCase`, parameter/local은 `camelCase`, public은 `PascalCase`.

## 금지/주의 이름

| 피할 이름 | 이유 | 대안 |
| --- | --- | --- |
| `GeneralManager`, `VariousManager` | 책임이 드러나지 않음 | 역할별 `System`/`Service`/`Controller` |
| `DataManager` | Content, Profile, cache, runtime state가 섞임 | `ContentCatalog`, `PlayerProfile`, `Repository` |
| `GameManager` | 앱/세션/상태 중 무엇인지 모호 | `AppRoot`, `MatchSession`, `MatchState` |
| `Util`, `Helper`, `Common` | 소유/책임이 사라짐 | 구체적인 도메인명 |
| `BaseXxx` | 상속 목적이 불분명 | interface 또는 composition |
| static `Instance` | 숨은 의존성과 테스트 오염 | Bootstrap 주입 또는 명시 parameter |

Unity/외부 SDK가 정한 이름(`NetworkManager`, `NetworkObject`)은 해당 Adapter 내부에서만
그대로 사용해도 된다.
