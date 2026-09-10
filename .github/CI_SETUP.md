# GitHub Actions CI 설정

`PR CI`는 두 단계로 동작한다.

1. `Repository checks`: 별도 Secret 없이 항상 실행
2. `Unity EditMode test and Windows build`: Unity License 설정 후 실행

## 항상 실행되는 검사

- Unity 프로젝트 필수 폴더와 파일
- Unity 버전 `6000.3.23f1`
- `Force Text`와 `Visible Meta Files` 설정
- 활성화된 Build Scene과 Scene `.meta`
- `Assets` 아래 모든 파일·폴더의 `.meta` 쌍
- 대상 Asset이 없는 고아 `.meta`
- Git에 들어가면 안 되는 `Library`, `Temp`, `Logs`, `obj`, `.vs`, `.idea`, `UserSettings`
- 해결되지 않은 Merge conflict 표시
- 50MB 초과 Asset 경고

Branch rule의 Required status check에는 우선 `Repository checks`만 지정한다. 이 검사는 Unity License가 없어도 항상 실행된다.

## 실제 Unity Test와 Windows Build 켜기

GitHub 저장소에서 다음 위치로 이동한다.

`Settings → Secrets and variables → Actions`

### Variables

| 이름 | 값 |
|---|---|
| `UNITY_CI_ENABLED` | `true` |

### Repository secrets

Unity Personal License 기준으로 다음 Secret을 등록한다.

| 이름 | 내용 |
|---|---|
| `UNITY_LICENSE` | GameCI 방식으로 발급한 Unity License 내용 |
| `UNITY_EMAIL` | Unity 계정 Email |
| `UNITY_PASSWORD` | Unity 계정 Password |

세 항목 중 하나라도 없으면 Unity Job은 어떤 Secret이 빠졌는지만 표시하고 실패한다. Secret 값 자체는 출력하지 않는다.

설정이 완료되면 Branch rule에 `Unity EditMode test and Windows build`도 Required status check로 추가할 수 있다.

## 현재 Build 대상

`ProjectSettings/EditorBuildSettings.asset`에서 활성화된 Scene 전체를 Windows `StandaloneWindows64`로 Build한다. 결과물은 `TheDeveloper-Windows` Artifact로 7일간 보관한다.

Unity 버전을 올릴 때는 다음 두 곳을 함께 변경한다.

- `ProjectSettings/ProjectVersion.txt`
- `.github/workflows/pr-ci.yml`의 `UNITY_VERSION`

GameCI 참고 문서:

- https://game.ci/docs/github/test-runner/
- https://game.ci/docs/github/builder/
