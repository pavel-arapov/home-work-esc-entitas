//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameStateEntity {

    public GameOverScreen gameOverScreen { get { return (GameOverScreen)GetComponent(GameStateComponentsLookup.GameOverScreen); } }
    public bool hasGameOverScreen { get { return HasComponent(GameStateComponentsLookup.GameOverScreen); } }

    public void AddGameOverScreen(UnityEngine.Canvas newValue) {
        var index = GameStateComponentsLookup.GameOverScreen;
        var component = (GameOverScreen)CreateComponent(index, typeof(GameOverScreen));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGameOverScreen(UnityEngine.Canvas newValue) {
        var index = GameStateComponentsLookup.GameOverScreen;
        var component = (GameOverScreen)CreateComponent(index, typeof(GameOverScreen));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGameOverScreen() {
        RemoveComponent(GameStateComponentsLookup.GameOverScreen);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameStateMatcher {

    static Entitas.IMatcher<GameStateEntity> _matcherGameOverScreen;

    public static Entitas.IMatcher<GameStateEntity> GameOverScreen {
        get {
            if (_matcherGameOverScreen == null) {
                var matcher = (Entitas.Matcher<GameStateEntity>)Entitas.Matcher<GameStateEntity>.AllOf(GameStateComponentsLookup.GameOverScreen);
                matcher.componentNames = GameStateComponentsLookup.componentNames;
                _matcherGameOverScreen = matcher;
            }

            return _matcherGameOverScreen;
        }
    }
}
